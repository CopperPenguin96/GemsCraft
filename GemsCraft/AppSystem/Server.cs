// Copyright 2009-2012 Matvei Stefarov <me@matvei.org>
// Modified LegendCraft Team
// Modified by apotter96 for GemsCraft

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Threading;
using GemsCraft.AppSystem.Logging;
using GemsCraft.AppSystem.Types;
using GemsCraft.Configuration;
using GemsCraft.Network;
using GemsCraft.Network.Packets;
using GemsCraft.Players;
using GemsCraft.Utils;
using GemBlocks.Blocks;
using GemBlocks.Levels;
using GemBlocks.Worlds;

namespace GemsCraft.AppSystem
{
    public enum SessionState { Handshaking, Login, Status, Play }
    
    /// <summary>
    /// Core of a GemsCraft server. Manages startup/shutdown, online player
    /// sessions, and global events and scheduled tasks.
    /// </summary>
    public partial class Server
    {
        public static DateTime StartTime { get; private set; }
        internal static int MaxUploadSpeed;
        internal static int BlockUpdateThrottling;

        internal const int MaxSessionPacketsPerTick = 128;
        internal const int MaxBlockUpdatesPerTick = 100000;
        internal static float TicksPerSecond;
        public static bool IsShuttingDown { get; internal set; }
        public static bool IsRestarting { get; internal set; }
        // public static List<Bot> Bots = new List<Bot>();

        public static bool AutoRankEnabled = false;
        public static bool IsRestarted = false;
        public static bool HeartbeatSaverOn = false;
        public static bool Moderation = false;

        public static PlayerList VoicedPlayers = new PlayerList();
        public static PlayerList TempBans = new PlayerList();
        public static PlayerList IPBans = new PlayerList();

        public static List<string> Entities = new List<string>();
        public static List<int> EntityIDs = new List<int>();

        // Networking
        public static IPAddress InternalIP { get; private set; }
        public static IPAddress ExternalIP { get; private set; }
        public static int Port { get; private set; }

        public static bool IsUsingMono => MonoCompat.IsMono;

        #region Iniitialization and Startup
        
        private static bool _serverInit;

        public static bool IsRunning { get; private set; }

        /// <summary>
        /// Initialized various server subsystems. This should be called after InitLibrary and before StartServer.
        /// Loads config, PlayerDB, IP bans, AutoRank settings, builds a list of commands, and prepares the IRC bot.
        /// Raises Server.Initializing and Server.Initialized events, and possibly Logger.Logged events.
        /// Throws exceptions on failure.
        /// </summary>
        /// <exception cref="System.InvalidOperationException"> Library is not initialized, or server is already initialzied. </exception>
        public static void InitServer(bool fromGui)
        {
            // Load Config before continuing,
            // if Config does not exist, loads defaults
            if (!Config.TryLoad())
            {
                throw new Exception("GemsCraft Config failed to initialize.");
            }

            
            Files.CheckPaths(); // Ensures all paths are ready for the server to start
            Logger.AddToFull("-------------Started Session on " +
                             DateTime.Now.ToLongDateString() + " @ " + DateTime.Now.ToLongTimeString() +
                             "-------------", true);
            Logger.Write("GemsCraft is starting...", LogType.System);

            BlockRegistry.Load();
            if (_serverInit)
            {
                if (!fromGui) throw new InvalidOperationException("Server is already initialized.");
            }

            RaiseEvent(Initializing);

            // TODO: Load Texture Packs

            // Version checking
            if (Config.Basic.AutoUpdateChecker)
            {
                Logger.Write("Checking for the latest GemsCraft updates...", LogType.System);
                Version online = Version.CheckLatest();
                int chk = Version.Compare(online, Version.LatestStable);
                switch (chk)
                {
                    case 0: // Updated
                        Logger.Write("Your GemsCraft is up-to-date!", LogType.System);
                        break;
                    case 1: // Outdated
                        Logger.Write(
                            "Your GemsCraft is outdated :( Type /update once the server starts to update your server.",
                            LogType.Warning);
                        break;
                    case 2: // Unreleased/pre-release
                        Logger.Write(
                            "You are using an unreleased/pre-release version of GemsCraft. It is recommended that you tread lightly." +
                            " We recommend you create a backup before doing anything else.",
                            LogType.Warning);
                        break;
                }
            }
            else
            {
                Logger.Write("Update Checks are disabled in your conig. It is recommended you enable this feature " +
                             "if you would like to be notified of future GemsCraft updates.", LogType.Warning);
            }

            // Encryption setup, and warning if not enabled.
            if (Config.Security.EnableEncryption)
            {
                CryptoServerProvider = new RSACryptoServiceProvider(1024);
                ServerKey = CryptoServerProvider.ExportParameters(true);
            }
            else
            {
                Logger.Write("Encryption is not enabled. For max security, it is recommended you enable encryption.",
                    LogType.Warning);
            }

            // Instantiate DeflateStream to make sure that libMonoPosix is present.
            // This allows catching misconfigured Mono installs early, and stopping the server.
            using (var testMemStream = new MemoryStream())
            {
                using (new DeflateStream(testMemStream, CompressionMode.Compress))
                {
                }
            }

            // Warning/disclaimers
            if (MonoCompat.IsMono && !MonoCompat.IsSGenCapable)
            {
                Logger.Write(
                    $"You are using a relatively old version of the Mono runtime ({MonoCompat.MonoVersion}). " +
                           "It is recommended that you upgrade to at least 2.8+", LogType.Warning);
            }

            // Load PlayerDB
            PlayerDatabase.Load();
            
        }
        
        #endregion

        public static PlayerList OnlinePlayers = new PlayerList();
        protected internal static RSACryptoServiceProvider CryptoServerProvider { get; set; }
        protected internal static RSAParameters ServerKey { get; set; }
        protected internal static string ID = " ";

        /// <summary>
        /// To be used by any basic random functions to improve
        /// randomness of functions
        /// </summary>
        public static Random Random = new Random();
        public static void Start()
        {
            //LoadMainWorld(null, null);
            Thread serverThread = new Thread(Run);
            serverThread.Start();
        }

        private static void Run()
        {
            IPAddress ip = IPAddress.Parse("0.0.0.0");
            int port = 25565;
            TcpListener server = new TcpListener(ip, port);

            Logger.Write("Starting the server on " + ip + ":" + port);

            try
            {
                server.Start();
                Logger.Write("Now accepting connections.");
                while (true)
                {
                    if (!server.Pending()) continue;
                    Thread tmpThread = new Thread(() =>
                    {
                        Player client = new Player(server.AcceptTcpClient());

                        using (NetworkStream ns = client.Client.GetStream())
                        {
                            GameStream stream = new GameStream(ns)
                            {
                                State = SessionState.Handshaking
                            };
                            using (StreamReader sr = new StreamReader(stream))
                            {
                                while (true)
                                {
                                    // Read packets here and transport them to their appropiate places
                                    VarInt length = stream.ReadVarInt();
                                    MemoryStream ms = new MemoryStream(stream.ReadByteArray((int) length.Value));
                                    Protocol.Receive(client, new GameStream(ms));
                                }
                            }
                        }

                    });
                    tmpThread.Start();
                }
            }
            catch (Exception e)
            {
                Logger.Write("Error!");
                Logger.Write(e.ToString());
            }
        }

        public static void LoadMainWorld(Player player, GameStream stream)
        {
            IGenerator gen = SimpleGenerator.DEFAULT;
            Level level = new Level("MainWorld", gen);
            level.setGameType(GameType.SURVIVAL);
            level.setAllowCommands(true);
            level.setMapFeatures(true);
            level.setSpawnPoint(0, 0, 0);
            World w = new World(level);
            Chunk chunk = w.getRegion(0, 0, true).getChunk(0, 0, true);
            PlayPackets.SendChunkData(player, stream,
                chunk);
            w.save();
        }
    }
}
