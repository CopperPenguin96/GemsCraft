using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem;
using Newtonsoft.Json;

namespace GemsCraft.Configuration
{
    /*
     * Config Versions
     *
     * d0 - Initial config
     */
    public class Config
    {
        public const string CurrentVersion = "d0"; // D for java-edition
        
        public Config()
        {
            if (!File.Exists(Files.ConfigName))
            {
                LoadDefaults();
                Logger.Log("Config.Load: No config.json was found. Loading defaults.");
            }
            else
            {
                try
                {
                    Server.Config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(Files.ConfigName));
                    string code = Server.Config.FileVersion.ToLower();
                    if (code != "d")
                    {
                        throw new Exception(
                            "Config.Load: Unsupported Config. Please use a valid config for this software.");
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(ex.ToString());
                    LoadDefaults();
                    Logger.Log("Config.Load: Could not load supplied config.json. Loading defaults.");
                }
            }
        }

        public void LoadDefaults()
        {
            GeneralDefaults();
            AdvancedDefaults();
        }

        private void GeneralDefaults()
        {
            Name = "[GemsCraft] Default";
            MOTD = "Welcome to the server!";
            FileVersion = CurrentVersion;
            Port = 25565;
            MaxPlayers = 20;
        }

        private void AdvancedDefaults()
        {
            PacketLogs = false;
        }

        public void Save()
        {
            var writer = File.CreateText(Files.ConfigName);
            string json = JsonConvert.SerializeObject(Server.Config, Formatting.Indented);
            writer.WriteLine(json);
            writer.Flush();
            writer.Close();
        }

        public bool TrySave()
        {
            try
            {
                Save();
                return true;
            }
            catch (Exception e)
            {
                Logger.Log(LogType.Error, e.ToString());
                return false;
            }
        }

        // General Configuration
        public string Name;
        public string MOTD;
        public string FileVersion;
        public int Port;
        public int MaxPlayers;

        // Advanced Configuration
        public bool PacketLogs;
    }
}
