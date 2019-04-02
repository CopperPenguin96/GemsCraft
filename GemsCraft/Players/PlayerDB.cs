using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem;
using Newtonsoft.Json;

namespace GemsCraft.Players
{
    internal class PlayerDB
    {
        public static List<Player> OfflinePlayers = new List<Player>();

        public static List<Player> AllPlayers()
        {
            List<Player> newList = new List<Player>();
            newList.AddRange(OfflinePlayers);
            newList.AddRange(Server.OnlinePlayers);
            return newList;
        }

        public static void SavePlayer(Player p)
        {
            string path = Files.PlayerDatabaseDir + p.UUID + ".json";
            var writer = File.CreateText(path);
            writer.WriteLine(JsonConvert.SerializeObject(p, Formatting.Indented));
            writer.Flush();
            writer.Close();
        }

        public static bool TrySavePlayer(Player p)
        {
            try
            {
                SavePlayer(p);
                return true;
            }
            catch (Exception e)
            {
#if DEBUG 
                Console.WriteLine(e);
#endif
                Logger.Log(LogType.PlayerDBError, Player.Console, $"Unable to save player to db ({p.UUID}).");
                return false;
            }
        }

        public static void SaveAll()
        {
            foreach (Player p in AllPlayers())
            {
                SavePlayer(p);
            }
        }

        public static bool TrySaveAll()
        {
            try
            {
                SaveAll();
                return false;
            }
            catch (Exception e)
            {
#if DEBUG
                Console.WriteLine(e);
#endif
                Logger.Log(LogType.PlayerDBError, Player.Console, $"Unable to save the Player Database.");
                return false;
            }
        }

        public static void LoadPlayerDB()
        {
            Server.KickAll();
            foreach (string file in Directory.GetFiles(Files.PlayerDatabaseDir))
            {
                string json = File.ReadAllText(file);
                Player p = JsonConvert.DeserializeObject<Player>(json);
                OfflinePlayers.Add(p);
            }
        }
    }
}
