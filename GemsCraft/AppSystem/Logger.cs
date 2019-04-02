using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.Players;

namespace GemsCraft.AppSystem
{
    public class Logger
    {
        public static List<Log> Logs = new List<Log>();

        public static void Log(LogType type, string message, Player player)
        {
            if (player == null) player = Player.Console;
            if (message == null) throw new ArgumentNullException(nameof(message));
            Log l = new Log {Sender = player, Time = DateTime.Now, Message = message};
            Console.WriteLine(l.ToString());
            Logs.Add(l);
        }

        public static void Log(string message, Player player)
        {
            if (player == null) player = Player.Console;
            if (message == null) throw new ArgumentNullException(nameof(message));
            Log(LogType.Normal, message, player);
        }

        public static void Log(LogType type, string message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));
            Log(type, message, Player.Console);
        }

        public static void Log(string message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));
            Log(LogType.Normal, message, Player.Console);
        }

        private static string[] LogsAsStrings()
        {
            List<string> logs = new List<string>();
            foreach (Log log in Logs)
            {
                logs.Add(log.ToString());
            }

            return logs.ToArray();
        }
        public static LogSaveType SaveType;
        public static void Save()
        {
            string file;
            Log(LogType.SystemActivity, "Logs are being saved.");
            switch (SaveType)
            {
                case LogSaveType.ByDay:
                    file = Files.LogDir + DateTime.Now.ToLongDateString() + ".txt";
                    break;
                case LogSaveType.ByHour:
                    file = Files.LogDir + DateTime.Now.ToLongDateString() + " " + DateTime.Now.Hour + ".txt";
                    break;
                case LogSaveType.BySession:
                    file = Files.LogDir + "Session" + Directory.GetFiles(Files.LogDir).Length + ".txt";
                    break;
                default:
                    file = Files.LogDir + "GemsCraftLog.txt";
                    break;
            }
            if (!File.Exists(file))
            {
                File.WriteAllLines(file, LogsAsStrings());
            }
            else
            {
                List<string> oldLogs = File.ReadAllLines(file).ToList();
                oldLogs.AddRange(LogsAsStrings());
                File.WriteAllLines(file, oldLogs);
            }
        }
    }

    public enum LogSaveType
    {
        OneFile, ByDay, ByHour, BySession
    }

    public enum LogType
    {
        Normal, SystemActivity, ServerStartup, Error, PlayerDBError
    }

    public struct Log
    {
        public Player Sender;
        public DateTime Time;
        public string Message;

        public override string ToString()
        {
            return $"[{Time.ToShortTimeString()}] ({Sender.Username}) {Message}";
        }
    }
}
