using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using GemsCraft.ChatSystem;
using GemsCraft.Configuration;
using JetBrains.Annotations;

namespace GemsCraft.AppSystem.Logging
{
    class Logger
    {
        public static List<string> FullLogs = new List<string>(); // What is saved to the files, includes full stamps
        public static List<string> RepresentedLogs = new List<string>(); // What is shown to the console

        /// <summary>
        /// Used to make an easy port of old fCraft-based code
        /// </summary>
        public static void Log(LogType type, string message)
        {
            Write(message, type);
        }

        [StringFormatMethod("message")]
        public static void Log(LogType type, string message, params object[] args)
        {
            Write(message, type, args);
        }

        /// <summary>
        /// Writes to the logger as a Normal log. Automatically saves the log.
        /// </summary>
        /// <param name="message">The text of the log</param>
        public static void Write(string message)
        { 
            Write(message, LogType.Normal);
        }

        [StringFormatMethod("message")]
        public static void Write(string message, params object[] args)
        {
            Write(string.Format(message, args));
        }

        [StringFormatMethod("message")]
        public static void Write(string message, LogType type, params object[] args)
        {
            Write(string.Format(message, args), type);
        }

        /// <summary>
        /// Writes to the logger as a Normal log. Saves if specificed true
        /// </summary>
        /// <param name="message">The text of the log</param>
        /// <param name="save">Whether to save the log or not</param>
        public static void Write(string message, bool save)
        {
            Write(message, LogType.Normal, save);
        }

        /// <summary>
        /// Writes to the logger as the specified log. Saves if specified true
        /// </summary>
        /// <param name="message">The text of the log</param>
        /// <param name="type">The type of the log</param>
        /// <param name="save">Whether to save the log or not</param>
        public static void Write(string message, LogType type, bool save)
        {
            string full = GetStamp(type, true) + message;
            string repr = GetStamp(type, false) + message;
            FullLogs.Add(full);
            RepresentedLogs.Add(repr);
            Console.ForegroundColor = ChatColor.ToConsoleColor(GetTypeColor(type));
            Console.WriteLine(repr);
            AddToFull(full, save);
        }

        /// <summary>
        /// Writes to the logger as the specified log. Automatically saves the log
        /// </summary>
        /// <param name="message">The text of the log</param>
        /// <param name="type">The type of the log</param>
        public static void Write(string message, LogType type)
        {
            Write(message, type, true);
        }


        public static void AddToFull(string s, bool save)
        {
            FullLogs.Add(s);
            if (!Config.Logging.SaveLogs) return;
            string fileName = Config.Logging.LogDirectory + DateTime.Now.ToLongDateString() + ".txt";
            var writer = File.Exists(fileName) ? File.AppendText(fileName) : File.CreateText(fileName);
            writer.WriteLine(s);
            writer.Flush();
            writer.Close();
        }

        private static string Timestamp()
        {
            return $"<{DateTime.Now.ToShortDateString()}, {DateTime.Now.ToShortTimeString()}> ";
        }

        /// <summary>
        /// Gets the time stamp + the LogType stamp if applicable
        /// </summary>
        /// <param name="type">LogType being represented</param>
        /// <param name="showName">Whether to show the actual name of the logtype. If set to false, reverts to default names, which some have none.</param>
        /// <returns>Log stamp in full context.</returns>
        private static string GetStamp(LogType type, bool showName)
        {
            string stamp = "";
            if (!showName)
            {
                switch (type)
                {
                    case LogType.ConsoleInput:
                    case LogType.ConsoleOutput:
                        stamp = "Console";
                        break;
                    case LogType.Trace:
                        stamp = "Trace";
                        break;
                    case LogType.Debug:
                        stamp = "Debug";
                        break;
                    case LogType.Warning:
                        stamp = "WARNING";
                        break;
                    case LogType.SuspiciousActivity:
                        stamp = "SUSPICIOUS ACTIVITY";
                        break;
                    case LogType.IRC:
                        stamp = "IRC";
                        break;
                    case LogType.PrivateChat:
                        stamp = "Private";
                        break;
                    case LogType.Discord:
                        stamp = "Discord";
                        break;
                    case LogType.Error:
                        stamp = "Error";
                        break;
                    case LogType.SeriousError:
                        stamp = "ERROR";
                        break;
                    case LogType.RankChat:
                        stamp = "Rank Chat";
                        break;
                    case LogType.GlobalChat:
                        stamp = "Global";
                        break;
                }
            }
            else
            {
                stamp = type.ToString();
            }

            if (stamp != "") stamp = "[" + stamp + "]";
            return Timestamp() + stamp + " ";
        }
        public static string GetTypeColor(LogType type)
        {
            // TODO update with config settings, + defaults
            switch (type)
            {
                case LogType.System:
                    return Config.Logging.SystemColor;
                case LogType.UserActivity:
                    return Config.Logging.UserActivityColor;
                case LogType.UserCommand:
                    return Config.Logging.UserCommandColor;
                case LogType.ConsoleInput:
                    return Config.Logging.ConsoleInputColor;
                case LogType.ConsoleOutput:
                    return Config.Logging.ConsoleOutputColor;
                case LogType.Trace:
                    return Config.Logging.TraceColor;
                case LogType.Debug:
                    return Config.Logging.DebugColor;
                case LogType.ChangedWorld:
                    return Config.Logging.ChangedWorldColor;
                case LogType.Warning:
                    return Config.Logging.WarningColor;
                case LogType.SuspiciousActivity:
                    return Config.Logging.SuspiciousActivityColor;
                case LogType.IRC:
                    return Config.Chat.IRCColor;
                case LogType.PrivateChat:
                    return Config.Chat.PrivateChatColor;
                case LogType.Discord:
                    return Config.Chat.DiscordColor;
                case LogType.Error:
                    return Config.Chat.ErrorColor;
                case LogType.SeriousError:
                    return Config.Chat.SeriousErrorColor;
                case LogType.RankChat:
                    return Config.Chat.RankChatColor;
                case LogType.GlobalChat:
                    return Config.Chat.GlobalChatColor;
                default:
                    return Config.Chat.DefaultColor;
            }

        }
        
    }
}
