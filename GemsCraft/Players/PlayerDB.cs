using System;
using System.IO;
using GemsCraft.AppSystem;
using GemsCraft.AppSystem.Logging;
using GemsCraft.Network;
using Newtonsoft.Json;

namespace GemsCraft.Players
{
    class PlayerDB
    {
        public static readonly PlayerList AllPlayers = new PlayerList();

        public static void Save()
        {
            foreach (Player p in AllPlayers)
            {
                p.Save();
            }
        }

        public static void TrySave()
        {
            try
            {
                Save();
            }
            catch (Exception e)
            {
                Logger.Write(e.ToString());
            }
        }

        public static void LoadPlayer(string username, GameStream stream, out Player pl)
        {
            bool exists = false;
            Player found = null;
            foreach (Player p in AllPlayers)
            {
                if (p.Username == username)
                {
                    exists = true;
                    found = p;
                }
            }
            if (!exists)
            {
                string uuid = Guid.NewGuid().ToString();
                Player player = new Player(username, uuid);
                player.Save();
                AllPlayers.Add(player);
                Server.OnlinePlayers.Add(player);
                pl = player;
            }
            else
            {
                Server.OnlinePlayers.Add(found);
                pl = found;
            }

        }

        public static void LoadPlayer(string username, GameStream stream)
        {
            LoadPlayer(username, stream, out _);
        }

        /// <summary>
        /// Reloads the PlayerDB, will kick all players
        /// </summary>
        public static void Load()
        {
            foreach (Player p in Server.OnlinePlayers)
            {
                // TODO - kick
            }
            AllPlayers.Clear();
            Server.OnlinePlayers.Clear();

            foreach (string file in Directory.GetFiles(Files.PlayerDatabasePath))
            {
                string json = File.ReadAllText(file);
                Player found = JsonConvert.DeserializeObject<Player>(json);
                AllPlayers.Add(found); // I once was lost, but now I'm found
            }
        }
    }
}
