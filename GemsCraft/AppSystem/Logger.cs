using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemsCraft.AppSystem
{
    class Logger
    {
        public static List<string> Logs = new List<string>();

        public static void Write(string message)
        {
            Console.WriteLine(Timestamp() + message);
        }

        private static string Timestamp()
        {
            return $"<{DateTime.Now.ToShortDateString()}, {DateTime.Now.ToShortTimeString()}> ";
        }
    }
}
