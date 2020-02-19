// Copyright 2009-2012 Matvei Stefarov <me@matvei.org>
// Changes Copyright 2020 Alex Potter <CopperPenquin96@gmail.com>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Net;
using System.Net.Cache;
using GemsCraft.Configuration;
using JetBrains.Annotations;
using System.Reflection;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Text;
using GemsCraft.AppSystem.Events;
using GemsCraft.Utils;
using java.nio.file;
using Path = System.IO.Path;

#if DEBUG_EVENTS
using System.Reflection.Emit;
#endif

namespace GemsCraft.AppSystem.Logging
{
    /// <summary>
    /// Central logging class. Logs to file, relays messages to the
    /// front end, submits crash reports.
    /// </summary>
    public static class Logger
    {
        /// <summary>
        /// Gets the name of the operating system
        /// </summary>
        public static string GetOperatingSystem()
        {
            string result = string.Empty;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem");
            foreach (var o in searcher.Get())
            {
                var os = (ManagementObject)o;
                result = os["Caption"].ToString();
                break;
            }
            return result;
        }

        private static readonly object LogLock = new object();

        public static bool Enabled { get; set; }
        public static readonly bool[] ConsoleOptions;
        public static readonly bool[] LogFileOptions;

        private const string DefaultLogName = "GemsCraft.log";
        private const string LongDateFormat = "yyyy'-'MM'-'dd'_'HH'-'mm'-'ss";
        private const string ShortDateFormat = "yyyy'-'MM'-'dd";

        private static readonly Uri CrashReportUri = new Uri("http://gemscraft.net/CrashReport/");
        public static LogSplittingType SplittingType = LogSplittingType.OneFile;
        private static readonly string SessionStart = DateTime.Now.ToString(LongDateFormat);

        private static readonly Queue<string> RecentMessages = new Queue<string>();
        private const int MaxRecentMessages = 25;

        public static string CurrentLogFileName
        {
            get
            {
                switch (SplittingType)
                {
                    case LogSplittingType.SplitBySession:
                        return SessionStart + ".log";
                    case LogSplittingType.SplitByDay:
                        return DateTime.Now.ToString(ShortDateFormat) + ".log";
                    default:
                        return DefaultLogName;
                }
            }
        }

        static Logger()
        {
            Enabled = true;
            int typeCount = Enum.GetNames(typeof(LogType)).Length;
            ConsoleOptions = new bool[typeCount];
            LogFileOptions = new bool[typeCount];

            for (int i = 0; i < typeCount; i++)
            {
                ConsoleOptions[i] = true;
                LogFileOptions[i] = true;
            }
        }

        internal static void MarkLogStart()
        {
            // Mark start of logging
            Log(LogType.SystemActivity,
                $"------ Log Starts {DateTime.Now.ToLongDateString()} ({DateTime.Now.ToShortDateString()}) ------");
        }

        public static void LogToConsole([NotNull] string message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));
            if (message.Contains("\n"))
            {
                foreach (string line in message.Split('\n'))
                {
                    LogToConsole(line);
                }

                return;
            }

            string processedMessage = "# ";
            for (int i = 0; i < message.Length; i++)
            {
                if (message[i] == '&') i++;
                else processedMessage += message[i];
            }

            Log(LogType.ConsoleOutput, processedMessage);
        }

        [DebuggerStepThrough]
        [StringFormatMethod("message")]
        public static void Log(LogType type, [NotNull] string message, [NotNull] params object[] values)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));
            if (values == null) throw new ArgumentNullException(nameof(values));
            Log(type, string.Format(message, values));
        }

        [DebuggerStepThrough]
        public static void Log(LogType type, [NotNull] string message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));
            if (!Enabled) return;

            string line = DateTime.Now.ToLongDateString() + " > " + GetPrefix(type) + message;

#if DEBUG
            Console.WriteLine(line);
#endif

            lock (LogLock)
            {
                RaiseLoggedEvent(message, line, type);
                RecentMessages.Enqueue(line);

                while (RecentMessages.Count > MaxRecentMessages)
                {
                    RecentMessages.Dequeue();
                }

                if (!LogFileOptions[(int)type]) return;

                try
                {
                    File.AppendAllText(Path.Combine(Paths.LogPath, CurrentLogFileName), line + Environment.NewLine);
                }
                catch (Exception ex)
                {
                    string errorMessage = "Logger.Log: " + ex.Message;
                    RaiseLoggedEvent(errorMessage,
                        DateTime.Now.ToLongTimeString() + " > " + GetPrefix(LogType.Error) + errorMessage, // localized
                        LogType.Error);
                }
            }
        }

        /// <summary>
        /// Sends a log like normal, but also sends a messagebox.
        /// </summary>
        [DebuggerStepThrough]
        public static void DualLog(LogType type, string message)
        {
            MessageBox.Show(message);
            Log(type, message);
        }

        [DebuggerStepThrough]
        public static string GetPrefix(LogType level)
        {
            switch (level)
            {
                case LogType.SeriousError:
                case LogType.Error:
                    return "ERROR: ";
                case LogType.Warning:
                    return "Warning: ";
                case LogType.IRC:
                    return "IRC: ";
                case LogType.Discord:
                    return "Discord: ";
                default:
                    return string.Empty;
            }
        }

        #region Crash Handling

        private static readonly object CrashReportLock = new object(); // mutex to prevent simultaneous reports (messes up the timers/requests)
        private static DateTime LastCrashReport = DateTime.MinValue;
        private const int MinCrashReportInterval = 61; // minimum interval between submitting crash reports, in seconds

        private struct CrashReportData
        {
            public string SoftwareVersion;
            public string Error;
            public string OperatingSystem;
            public string Runtime;
            public string ServerName;
            public string Exception;
            public string Config;
            public string Logs;
            public string Date;
            public string Time;
            public string Assembly;
            public bool ShutdownImminent;
        }

        private static void SaveReport(DateTime n, CrashReportData data)
        {
            const string folder = "Crash Reports/";
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string file = $"{folder}crash_report.{ReportDateTime(n)}";
            var writer = File.CreateText(file);
            writer.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
            writer.Flush();
            writer.Close();
        }

        private static string ReportDateTime(DateTime n)
        {
            return $"{n.ToLongDateString()}.{n.ToLongTimeString()}.txt".Replace(",", "_").Replace(":", "-");
        }

        public static void LogAndReportCrash([CanBeNull] string message, [CanBeNull] string assembly,
            [CanBeNull] Exception exception, bool shutdownImminent)
        {
            DateTime n = DateTime.Now;
            string file = $"crash{ReportDateTime(n)}";

            if (message == null) message = "(null)";
            if (assembly == null) assembly = "(null)";
            if (exception == null) exception = new Exception("(null)");

            Log(LogType.SeriousError, "{0}: {1}", message, exception);

            bool submitCrashReport = Config.Advanced.SubmitCrashReports;
            bool isCommon = CheckForCommonErrors(exception);

            // ReSharper disable EmptyGeneralCatchClause
            try
            {
                var eventArgs = new CrashedEventArgs(message,
                                                      assembly,
                                                      exception,
                                                      submitCrashReport && !isCommon,
                                                      isCommon,
                                                      shutdownImminent);
                RaiseCrashedEvent(eventArgs);
                isCommon = eventArgs.IsCommonProblem;
            }
            catch { }
            // ReSharper restore EmptyGeneralCatchClause

            if (!submitCrashReport || isCommon)
            {
                return;
            }

            lock (CrashReportLock)
            {
                if (DateTime.UtcNow.Subtract(LastCrashReport).TotalSeconds < MinCrashReportInterval)
                {
                    Log(LogType.Warning, "Logger.SubmitCrashReport: Could not submit crash report, reports too frequent.");
                    return;
                }
                LastCrashReport = DateTime.UtcNow;

                try
                {
                    StringBuilder sb = new StringBuilder();
                    CrashReportData data = new CrashReportData
                    {
                        SoftwareVersion = Version.LatestStable.ToString(),
                        Error = message
                    };


                    if (MonoCompat.IsMono)
                    {
                        data.OperatingSystem = Environment.OSVersion.VersionString;
                        data.Runtime = "Mono " + MonoCompat.MonoVersionString;
                    }
                    else
                    {
                        data.OperatingSystem = GetOperatingSystem() + Environment.OSVersion.ServicePack;
                        data.Runtime =
                            $".Net {".Net " + Environment.Version.Major + "." + Environment.Version.MajorRevision + "." + Environment.Version.Build}";
                    }
                    data.ServerName = Config.Basic.ServerName;


                    switch (exception)
                    {
                        case TargetInvocationException _:
                        case TypeInitializationException _:
                            exception = (exception).InnerException;
                            break;
                    }

                    if (exception != null)
                        data.Exception = exception.GetType() + ": " + exception.Message + ", Stack: " +
                                         exception.StackTrace;

                    data.Config = File.Exists(Paths.ConfigFileName) ? Paths.ConfigFileName : "config.json not found";

                    string[] lastFewLines;
                    lock (LogLock)
                    {
                        lastFewLines = RecentMessages.ToArray();
                    }

                    data.Logs = string.Join(Environment.NewLine, lastFewLines);
                    string json = JsonConvert.SerializeObject(data, Formatting.Indented);
                    string postData = $"?json={Uri.EscapeDataString(json)}";
                    byte[] formData = Encoding.UTF8.GetBytes(postData);

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(CrashReportUri);
                    request.Method = "POST";
                    request.Timeout = 15000; // 15s timeout
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
                    request.ContentLength = formData.Length;

                    using (Stream requestStream = request.GetRequestStream())
                    {
                        requestStream.Write(formData, 0, formData.Length);
                        requestStream.Flush();
                    }

                    string responseString;
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        using (Stream responseStream = response.GetResponseStream())
                        {
                            // ReSharper disable AssignNullToNotNullAttribute
                            using (StreamReader reader = new StreamReader(responseStream))
                            {
                                // ReSharper restore AssignNullToNotNullAttribute
                                responseString = reader.ReadLine();
                            }
                        }
                    }
                    request.Abort();

                    if (responseString != null && responseString.StartsWith("ERROR"))
                    {
                        Log(LogType.Error, "Crash report could not be processed by gemscraft.net.");
                    }
                    else
                    {
                        if (responseString != null && int.TryParse(responseString, out var referenceNumber))
                        {
                            Log(LogType.SystemActivity, "Crash report submitted (Reference #{0})", referenceNumber);
                        }
                        else
                        {
                            Log(LogType.SystemActivity, "Crash report submitted.");
                        }
                    }


                }
                catch (Exception ex)
                {
                    Log(LogType.Warning, "Logger.SubmitCrashReport: {0}", ex.Message);
                }
            }
        }

        /// <summary>
        /// Called by the Logger in case of serious errors to print
        /// troubleshooting advice.
        /// Returns true if the eerror is common, and report should NOT
        /// be submitted.
        /// </summary>
        public static bool CheckForCommonErrors([CanBeNull] Exception ex)
        {
            if (ex == null) throw new ArgumentNullException(nameof(ex));
            string message = null;
            try
            {
                if (ex is FileNotFoundException && ex.Message.Contains("Version=3.5"))
                {
                    message = "Your crash was likely caused by using a wrong version of .NET or Mono runtime. " +
                              "Please update to Microsoft .NET Framework 3.5 (Windows) OR Mono 2.6.4+ (Linux, Unix, Mac OS X).";
                    return true;

                }
                else if (ex.Message.Contains("libMonoPosixHelper") ||
                         ex is EntryPointNotFoundException && ex.Message.Contains("CreateZStream"))
                {
                    message = "GemsCraft could not locate Mono's compression functionality. " +
                              "Please make sure that you have zlib (sometimes called \"libz\" or just \"z\") installed. " +
                              "Some versions of Mono may also require \"libmono-posix-2.0-cil\" package to be installed.";
                    return true;

                }
                else if (ex is MissingMemberException || ex is TypeLoadException)
                {
                    message = "Something is incompatible with the current revision of GemsCraft. " +
                              "If you installed third-party modifications, " +
                              "make sure to use the correct revision (as specified by mod developers). " +
                              "If your own modifications stopped working, your may need to make some updates.";
                    return true;

                }
                else if (ex is UnauthorizedAccessException)
                {
                    message = "GemsCraft was blocked from accessing a file or resource. " +
                              "Make sure that correct permissions are set for the GemsCraft files, folders, and processes.";
                    return true;

                }
                else if (ex is OutOfMemoryException)
                {
                    message = "GemsCraft ran out of memory. Make sure there is enough RAM to run.";
                    return true;

                }
                else if (ex is SystemException && ex.Message == "Can't find current process")
                {
                    // Ignore Mono-specific bug in MonitorProcessorUsage()
                    return true;

                }
                else if (ex is InvalidOperationException && ex.StackTrace.Contains("MD5CryptoServiceProvider"))
                {
                    message = "Some Windows settings are preventing GemsCraft from doing player name verification. " +
                              "See http://support.microsoft.com/kb/811833";
                    return true;

                }
                else if (ex.StackTrace.Contains("__Error.WinIOError"))
                {
                    message = "A filesystem-related error has occured. Make sure that only one instance of GemsCraft is running, " +
                              "and that no other processes are using server's files or directories.";
                    return true;

                }
                else if (ex.Message.Contains("UNSTABLE"))
                {
                    return true;

                }
                else
                {
                    return false;
                }
            }
            finally
            {
                if (message != null)
                {
                    Log(LogType.Warning, message);
                }
            }
        }

        #endregion

        #region Event Tracing

#if DEBUG_EVENTS
        /// <summary>
        /// List of events in this assembly
        /// </summary>
        private static readonly Dictionary<int, EventInfo> EventsMap = new Dictionary<int, EventInfo>();
        private static readonly List<string> EventWhitelist = new List<string>();
        private static readonly List<string> EventBlacklist = new List<string>();
        private const string TraceWhitelistFile = "traceonly.txt",
                     TraceBlacklistFile = "notrace.txt";
        private static bool _useEventWhitelist, _useEventBlacklist;

        private static void LoadTracingSettings()
        {
            if (File.Exists(TraceWhitelistFile))
            {
                _useEventWhitelist = true;
                EventWhitelist.AddRange(File.ReadAllLines(TraceWhitelistFile));
            }
            else if (File.Exists(TraceBlacklistFile))
            {
                _useEventBlacklist = true;
                EventBlacklist.AddRange(File.ReadAllLines(TraceBlacklistFile));
            }
        }

        /// <summary>
        /// Adds hooks to all compliant events in current assembly
        /// </summary>
        internal static void PrepareEventTracing()
        {

            LoadTracingSettings();

            // create a dynamic type to hold our handler methods
            AppDomain myDomain = AppDomain.CurrentDomain;
            var asmName = new AssemblyName("fCraftEventTracing");
            AssemblyBuilder myAsmBuilder = myDomain.DefineDynamicAssembly(asmName, AssemblyBuilderAccess.RunAndSave);
            ModuleBuilder myModule = myAsmBuilder.DefineDynamicModule("DynamicHandlersModule");
            TypeBuilder typeBuilder = myModule.DefineType("EventHandlersContainer", TypeAttributes.Public);

            int eventIndex = 0;
            Assembly asm = Assembly.GetExecutingAssembly();
            List<EventInfo> eventList = new List<EventInfo>();

            // find all events in current assembly, and create a handler for each one
            foreach (Type type in asm.GetTypes())
            {
                foreach (EventInfo eventInfo in type.GetEvents())
                {
                    // Skip non-static events
                    if ((eventInfo.GetAddMethod().Attributes & MethodAttributes.Static) != MethodAttributes.Static)
                    {
                        continue;
                    }

                    if (!eventInfo.EventHandlerType.FullName.StartsWith(typeof(EventHandler<>).FullName) &&
                        !eventInfo.EventHandlerType.FullName.StartsWith(typeof(EventHandler).FullName)) continue;
                    if (_useEventWhitelist && !EventWhitelist.Contains(type.Name + "." + eventInfo.Name, StringComparer.OrdinalIgnoreCase) ||
                        _useEventBlacklist && EventBlacklist.Contains(type.Name + "." + eventInfo.Name, StringComparer.OrdinalIgnoreCase)) continue;

                    MethodInfo method = eventInfo.EventHandlerType.GetMethod("Invoke");
                    var parameterTypes = method.GetParameters().Select(info => info.ParameterType).ToArray();
                    AddEventHook(typeBuilder, parameterTypes, method.ReturnType, eventIndex);
                    eventList.Add(eventInfo);
                    EventsMap.Add(eventIndex, eventInfo);
                    eventIndex++;
                }
            }

            // hook up the handlers
            Type handlerType = typeBuilder.CreateType();
            for (int i = 0; i < eventList.Count; i++)
            {
                MethodInfo notifier = handlerType.GetMethod("EventHook" + i);
                var handlerDelegate = Delegate.CreateDelegate(eventList[i].EventHandlerType, notifier);
                try
                {
                    eventList[i].AddEventHandler(null, handlerDelegate);
                }
                catch (TargetException)
                {
                    // There's no way to tell if an event is static until you
                    // try adding a handler with target=null.
                    // If it wasn't static, TargetException is thrown
                }
            }
        }
        
        /// <summary>
        /// Create a static handler method that matches the given signature, and calls EventTraceNotifier
        /// </summary>
        private static void AddEventHook(TypeBuilder typeBuilder, Type[] methodParams, Type returnType, int eventIndex)
        {
            string methodName = "EventHook" + eventIndex;
            MethodBuilder methodBuilder = typeBuilder.DefineMethod(methodName,
                                                                    MethodAttributes.Public | MethodAttributes.Static,
                                                                    returnType,
                                                                    methodParams);

            ILGenerator generator = methodBuilder.GetILGenerator();
            generator.Emit(OpCodes.Ldc_I4, eventIndex);
            generator.Emit(OpCodes.Ldarg_1);
            MethodInfo min = typeof(Logger).GetMethod("EventTraceNotifier");
            generator.EmitCall(OpCodes.Call, min, null);
            generator.Emit(OpCodes.Ret);
        }
        
        /// <summary>
        /// Invoked when events fire
        /// </summary>
        public static void EventTraceNotifier(int eventIndex, EventArgs e)
        {
            if (e is LogEventArgs args && args.MessageType == LogType.Trace) return;
            var eventInfo = EventsMap[eventIndex];

            StringBuilder sb = new StringBuilder();
            bool first = true;
            foreach (var prop in e.GetType().GetProperties())
            {
                if (!first) sb.Append(", ");
                if (prop.Name != prop.PropertyType.Name)
                {
                    sb.Append(prop.Name).Append('=');
                }
                object val = prop.GetValue(e, null);
                switch (val)
                {
                    case null:
                        sb.Append("null");
                        break;
                    case string _:
                        sb.AppendFormat("\"{0}\"", val);
                        break;
                    default:
                        sb.Append(val);
                        break;
                }
                first = false;
            }

            Log(LogType.Trace,
                 "TraceEvent: {0}.{1}( {2} )",
                 eventInfo.DeclaringType.Name, eventInfo.Name, sb.ToString());

        }
#endif

        #endregion

        #region Events

        /// <summary>
        /// Occurs after a message has been logged.
        /// </summary>
        public static event EventHandler<LogEventArgs> Logged;

        /// <summary>
        /// Occurs when the server crashes (unhandled exceptions).
        /// </summary>
        public static event EventHandler<CrashedEventArgs> Crashed;

        [DebuggerStepThrough]
        private static void RaiseLoggedEvent([NotNull] string rawMessage, [NotNull] string line, LogType logType)
        {
            if (rawMessage == null) throw new ArgumentNullException(nameof(rawMessage));
            if (line == null) throw new ArgumentNullException(nameof(line));
            var h = Logged;
            h?.Invoke(null, new LogEventArgs(rawMessage,
                line,
                logType,
                LogFileOptions[(int)logType],
                ConsoleOptions[(int)logType]));
        }

        private static void RaiseCrashedEvent(CrashedEventArgs e)
        {
            var h = Crashed;
            h?.Invoke(null, e);
        }

        #endregion
    }
}
