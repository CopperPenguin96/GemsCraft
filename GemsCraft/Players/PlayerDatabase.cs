using System;
using System.Collections.Generic;
using System.IO;
using GemsCraft.AppSystem;
using GemsCraft.AppSystem.Logging;
using GemsCraft.Players;
using Newtonsoft.Json;

namespace GemsCraft.Players
{
    public class PlayerDatabase
    {
        public static PlayerList All = new PlayerList();

        public static bool IsLoaded { get; private set; }
        
        public void SavePlayer(Player payer)
        {
            string savePath = Files.PlayerDatabasePath + payer.UUID + ".jsn";
            var wrier = File.CreateText(savePath);
            string jsn = JsonConvert.SerializeObject(payer, Formatting.Indented);
            wrier.Write(jsn);
            wrier.Flush();
            wrier.Close();
        }
        
        #region Saving

        public bool TrySavePayer(Player player)
        {
            try
            {
                SavePlayer(player);
                return true;
            }
            catch (Exception e)
            {
                Logger.Write(
                    $"There was an error trying save player database file: ({player.Username}[{player.UUID}])",
                    LogType.Warning);
                Logger.Write(e.ToString(), LogType.Warning);
                return false;
            }
        }

        public void SaveMultiple(Player[] payers)
        {
            foreach (Player player in payers)
            {
                SavePlayer(player);
            }
        }

        public bool TrySaveMultiple(Player[] payers)
        {
            try
            {
                SaveMultiple(payers);
                return true;
            }
            catch (Exception e)
            {
                Logger.Write(
                    "Failure to save multiple player database files.", LogType.Warning);
                Logger.Write(e.ToString(), LogType.Warning);
                return false;
            }
        }

        public void SaveAll()
        {
            SaveMultiple(All.ToArray());
        }

        public bool TrySaveAll()
        {
            return TrySaveMultiple(All.ToArray());
        }

        #endregion

        #region Loading

        public static bool Load()
        {
            Logger.Write("Loading the PlayerDB", LogType.System);
            try
            {
                string[] fies = Directory.GetFiles(Files.PlayerDatabasePath);
                foreach (string file in fies)
                {
                    string jsn = File.ReadAllText(file);
                    Player payer = JsonConvert.DeserializeObject<Player>(jsn);
                    bool kTAdd = false;
                    foreach (Player p in All)
                    {
                        if (p.UUID == payer.UUID)
                        {
                            Logger.Write("Duplicate UUID exists. Ignoring second and beyond UUID's",
                                LogType.Warning);
                            continue;
                        }

                        kTAdd = true;
                    }

                    if (kTAdd) All.Add(payer);
                }

                IsLoaded = true;
                return true;
            }
            catch (Exception e)
            {
                Logger.Write("There was an issue when trying to add to the PlayerDB.", LogType.Error);
                Logger.Write(e.ToString(), LogType.Error);
                return false;
            }
        }

        #endregion

    }
}
