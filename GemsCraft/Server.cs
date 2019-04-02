using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GemsCraft.AppSystem;
using GemsCraft.Players;

namespace GemsCraft
{
    public class Server
    {
        public static List<Player> OnlinePlayers = new List<Player>();

        public static void Start()
        {
            Thread serverThread = new Thread(ServerThread);
            serverThread.Start();
        }

        private static void ServerThread()
        {
            Player.Console = new Player {Username = "Console"};
            Logger.Log(LogType.ServerStartup, "Starting the Server...", Player.Console);
            CheckDirs();
            Logger.Log(LogType.ServerStartup, "Loading Player Database...");
            PlayerDB.LoadPlayerDB();
            Logger.Log(LogType.ServerStartup, "Saving Console Player...");
            PlayerDB.SavePlayer(Player.Console);
        }

        private static void CheckDirs()
        {
            if (!Directory.Exists(Files.BaseDir)) Directory.CreateDirectory(Files.BaseDir);
            if (!Directory.Exists(Files.LogDir)) Directory.CreateDirectory(Files.LogDir);
            if (!Directory.Exists(Files.PlayerDatabaseDir)) Directory.CreateDirectory(Files.PlayerDatabaseDir);
        }

        public static void KickAll()
        {
            // TODO - kick all players
        }

        public static bool PauseConnections { get; internal set; }
    }
}
