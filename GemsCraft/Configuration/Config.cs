using System;
using GemsCraft.AppSystem;
using GemsCraft.AppSystem.Logging;
using GemsCraft.Configuration.Categories;
using Newtonsoft.Json;
using Version = GemsCraft.AppSystem.Version;

namespace GemsCraft.Configuration
{
    /*
     * GemsCraft Modern Config History
     *
     * *-*-*-* Alpha Phase *-*-*-*
     * 0.0 - Basic Config, mostly taken from GemsCraft Classic
     */
    public class Config
    {
        public static BasicConfig Basic = new BasicConfig();
        public static ChatConfig Chat = new ChatConfig();
        public static WorldConfig Worlds = new WorldConfig();
        public static SecurityConfig Security = new SecurityConfig();
        public static LoggingConfig Logging = new LoggingConfig();
        public static AdvancedConfig Advanced = new AdvancedConfig();
        public static IRCConfig IRC = new IRCConfig();
        public static MiscConfig Misc = new MiscConfig();

        /// <summary>
        /// Loads the default configs
        /// </summary>
        public static void LoadDefaults()
        {
            Basic = new BasicConfig();
            Chat = new ChatConfig();
            Worlds = new WorldConfig();
            Security = new SecurityConfig();
            Logging = new LoggingConfig();
            Advanced = new AdvancedConfig();
            IRC = new IRCConfig();
            Misc = new MiscConfig();
        }

        /// <summary>
        /// Used for saving/loading the config via JSON
        /// </summary>
        public class ConfigFile
        {
            public string AppVersion { get; set; }
            public BasicConfig Basic { get; set; }
            public ChatConfig Chat { get; set; }
            public WorldConfig Worlds { get; set; }
            public SecurityConfig Security { get; set; }
            public LoggingConfig Logging { get; set; }
            public AdvancedConfig Advanced { get; set; }
            public IRCConfig IRC { get; set; }
            public MiscConfig Misc { get; set; }
        }

        private static readonly ConfigFile File = new ConfigFile();

        public static void Save()
        {
            File.AppVersion = Version.LatestStable.ToString();
            File.Basic = Basic;
            File.Chat = Chat;
            File.Worlds = Worlds;
            File.Security = Security;
            File.Logging = Logging;
            File.Advanced = Advanced;
            File.IRC = IRC;
            File.Misc = Misc;

            string json = JsonConvert.SerializeObject(File, Formatting.Indented);
            var writer = System.IO.File.CreateText(Files.ConfigurationPath);
            writer.Write(json);
            writer.Flush();
            writer.Close();
        }

        public static bool TrySave()
        {
            try
            {
                Save();
                return true;
            }
            catch (Exception e)
            {
                Logger.Write("Unable to save config", LogType.Error);
                Logger.Write(e.ToString(), LogType.Error);
                return false;
            }
        }

        public static void Load()
        {
            if (!System.IO.File.Exists(Files.ConfigurationPath))
            {
                Logger.Write("Configuration does not exist. Loading defaults.", LogType.System);
                LoadDefaults();
                return;
            }
            string json = System.IO.File.ReadAllText(Files.ConfigurationPath);
            ConfigFile file = JsonConvert.DeserializeObject<ConfigFile>(json);
            if (file == null)
            {
                Logger.Write("Unable to load Config. Loading defaults.", LogType.Warning);
                LoadDefaults();
            }
            else
            {
                Basic = file.Basic;
                Chat = file.Chat;
                Worlds = file.Worlds;
                Security = file.Security;
                Logging = file.Logging;
                Advanced = file.Advanced;
                IRC = file.IRC;
                Misc = file.Misc;
            }
        }

        public static bool TryLoad()
        {
            try
            {
                Load();
                return true;
            }
            catch (Exception e)
            {
                Logger.Write("Unable to load config. Loading defaults.", LogType.Warning);
                Logger.Write(e.ToString(), LogType.Warning);
                LoadDefaults();
                return false;
            }
        }
    }
}
