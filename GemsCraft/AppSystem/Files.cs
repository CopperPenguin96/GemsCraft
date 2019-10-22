using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemsCraft.AppSystem
{
    internal class Files
    {
        public const string MainPath = "GemsCraft/";
        public const string PlayerDatabasePath = "GemsCraft/PlayerDB/";
        public const string ConfigurationPath = "GemsCraft/config.json";
        public const string LogPath = "GemsCraft/Logs/";

        /// <summary>
        /// Checks to make sure the directories utilized by GemsCraft
        /// are created and if not creates them
        /// </summary>
        public static void CheckPaths()
        {
            if (!Directory.Exists(MainPath)) CreateDir(MainPath);
            if (!Directory.Exists(PlayerDatabasePath)) CreateDir(PlayerDatabasePath);
            if (!Directory.Exists(LogPath)) CreateDir(LogPath);
        }

        private static void CreateDir(string dir)
        {
            Directory.CreateDirectory(dir);
        }
    }
}
