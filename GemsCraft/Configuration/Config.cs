using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem;
using GemsCraft.AppSystem.Logging;
using GemsCraft.Chat;
using GemsCraft.Properties;
using GemsCraft.Worlds;
using Newtonsoft.Json;
using Version = GemsCraft.AppSystem.Version;

namespace GemsCraft.Configuration
{
    public class Config
    {
        public static Config Current = new Config();
        //public readonly string AppVersion = Version.LatestStable.ToString(true);

        // Basic
        public string ServerName { get; internal set; } = "[GemsCraft] Default";
        public bool OnlineMode { get; internal set; } = true;
        public bool AutoUpdateChecker { get; internal set; } = true;
        public ServerIcon Icon { get; internal set; } = new ServerIcon(Resources.server_icon);
        public string MOTD { get; internal set; } = "Welcome to the Server!";
        public int MaxPlayers { get; internal set; } = 20;
        public Difficulty Difficulty = Difficulty.Easy;

        private int _tWorld = 0;
        // Worlds
        private int _mWorld
        {
            get => 10;
            set
            {
                if (Current == null) return;
                else
                {
                    _tWorld = value;
                }
            }
        }

        public int MaxPerWorld
        {
            get => _mWorld;
            internal set
            {
                if (value > MaxPlayers)
                    throw new ArgumentOutOfRangeException(
                        "Max players per world cannot be bigger than max server players!");
                _mWorld = value;
            }
        }
        // Security
        public bool EnableEncryption { get; internal set; } = true;

        // Advanced
        public bool EnablePacketCompression { get; internal set; } = false;
        public string PlayerDBDirectory { get; internal set; } = Files.PlayerDatabasePath;
        public bool ShowAdvancedDebugInfo { get; internal set; } = false;

        // Logging
        public string LogDirectory { get; internal set; } = Files.LogPath;
        public bool SaveLogs { get; internal set; } = true;
        public string SystemColor { get; internal set; } = ChatColor.Gray;
        public string UserActivityColor { get; internal set; } = ChatColor.Gray;
        public string UserCommandColor { get; internal set; } = ChatColor.Gray;
        public string ConsoleInputColor { get; internal set; } = ChatColor.Gray;
        public string ConsoleOutputColor { get; internal set; } = ChatColor.Gray;
        public string TraceColor { get; internal set; } = ChatColor.Gray;
        public string DebugColor { get; internal set; } = ChatColor.Gray;
        public string ChangedWorldColor { get; internal set; } = ChatColor.Lime;
        public string WarningColor { get; internal set; } = ChatColor.Yellow;
        public string SuspiciousActivityColor { get; internal set; } = ChatColor.Yellow;
        public string IRCColor { get; internal set; } = ChatColor.Purple;
        public string PrivateChatColor { get; internal set; } = ChatColor.Purple;
        public string DiscordColor { get; internal set; } = ChatColor.Purple;
        public string ErrorColor { get; internal set; } = ChatColor.Red;
        public string SeriousErrorColor { get; internal set; } = ChatColor.Maroon;
        public string RankChatColor { get; internal set; } = ChatColor.Navy;
        public string GlobalChatColor { get; internal set; } = ChatColor.Green;
        public string DefaultColor { get; internal set; } = ChatColor.White;

        public static void LoadDefaults()
        {
            Current = new Config();
        }

        public static void Save()
        {
            string json = JsonConvert.SerializeObject(Current, Formatting.Indented);
            var writer = File.CreateText(Files.ConfigurationPath);
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
            if (!File.Exists(Files.ConfigurationPath))
            {
                Logger.Write("Configuration does not exist. Loading defaults.", LogType.System);
                LoadDefaults();
                return;
            }
            string json = File.ReadAllText(Files.ConfigurationPath);
            Current = JsonConvert.DeserializeObject<Config>(json);
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
