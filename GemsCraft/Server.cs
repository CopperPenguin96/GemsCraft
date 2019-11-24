using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Threading;
using GemsCraft.AppSystem;
using GemsCraft.AppSystem.Logging;
using GemsCraft.AppSystem.Types;
using GemsCraft.Configuration;
using GemsCraft.Network;
using GemsCraft.Network.Packets;
using GemsCraft.Players;
using minecraft.blocks;
using minecraft.level;
using minecraft.world;
using Version = GemsCraft.AppSystem.Version;

namespace GemsCraft
{
    public enum SessionState { Handshaking, Login, Status, Play }
    
    public class Server
    {
        public static PlayerList OnlinePlayers = new PlayerList();
        protected internal static RSACryptoServiceProvider CryptoServerProvider { get; set; }
        protected internal static RSAParameters ServerKey { get; set; }
        protected internal static string ID = " ";
        public static void Start()
        {
            //LoadMainWorld(null, null);
            Config.Load(); // The config must be the first thing loaded
            Files.CheckPaths(); // Ensures all paths are ready for the server to start
            Logger.AddToFull("-------------Started Session on " +
                             DateTime.Now.ToLongDateString() + " @ " + DateTime.Now.ToLongTimeString() +
                             "-------------", true);
            Logger.Write("GemsCraft is starting...", LogType.System);

            // Must be loaded before using any blocks :/
            BlockRegistry.Load();
            // Encryption setup, and warning if not enabled.
            if (Config.Current.EnableEncryption)
            {
                CryptoServerProvider = new RSACryptoServiceProvider(1024);
                ServerKey = CryptoServerProvider.ExportParameters(true);
            }
            else
            {
                Logger.Write("Encryption is not enabled. For max security, it is recommended you enable encryption.",
                    LogType.Warning);
            }

            // Version checking
            if (Config.Current.AutoUpdateChecker)
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
