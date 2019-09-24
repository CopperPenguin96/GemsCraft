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

        /// <summary>
        /// Checks to make sure the directories utilized by GemsCraft
        /// are created and if not creates them
        /// </summary>
        public static void CheckPaths()
        {
            if (!Directory.Exists(MainPath)) Directory.CreateDirectory(MainPath);
            if (!Directory.Exists(PlayerDatabasePath)) Directory.CreateDirectory(PlayerDatabasePath);
        }
    }
}
