using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.Players;

namespace GemsCraft.AppSystem
{
    public class Logger
    {
        public static List<Log> Logs = new List<Log>();

        public static void Log(LogType type, Player player, string message)
        {
            if (player == null) throw new ArgumentNullException(nameof(player));
            if (message == null) throw new ArgumentNullException(nameof(message));
            Log l = new Log {Sender = player, Time = DateTime.Now, Message = message};
            Console.WriteLine(l.ToString());
            Logs.Add(l);
        }

        public static void Save()
        {
            // TODO - save logs, (by day?)
        }
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
