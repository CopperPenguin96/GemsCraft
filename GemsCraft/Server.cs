using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Claims;
using System.Threading;
using GemsCraft.AppSystem;
using GemsCraft.Configuration;
using GemsCraft.Network;
using GemsCraft.Network.Packets;
using GemsCraft.Players;

namespace GemsCraft
{
    public class Server
    {
        public static List<Player> OnlinePlayers = new List<Player>();
        public static Config Config;
        public static IPAddress IP;

        public static void Start()
        {
            // Create console player
            Player.Console = new Player("Console");
            // Server thread
            Thread serverThread = new Thread(ServerThread);
            serverThread.Start();
        }

        private static void ServerThread()
        {
            Logger.Log(LogType.ServerStartup, "Starting the Server...", Player.Console);
            CheckDirs();

            Logger.Log(LogType.ServerStartup, "Loading the configuration...");
            Config = new Config();

            Logger.Log(LogType.ServerStartup, "Loading Player Database...");
            PlayerDB.LoadPlayerDB();

            bool listen = true;
            try
            {
                IPAddress ip = IPAddress.Parse("0.0.0.0");
                TcpListener listener = new TcpListener(ip, 25565);
                listener.Start();
                Logger.Log(LogType.ServerStartup, "Now accepting connections.");
                while (listen)
                {
                    
                    if (listener.Pending())
                    {
                        Thread tmpThread = new Thread(() =>
                        {
                            string msg = null;
                            TcpClient client = listener.AcceptTcpClient();
                            using (NetworkStream ns = client.GetStream())
                            {
                                GameStream gameStream = new GameStream(ns);
                                using (StreamReader sr = new StreamReader(gameStream))
                                {
                                    while (ns.DataAvailable)
                                    {
                                        VarInt length = gameStream.ReadVarInt();
                                        VarInt id = gameStream.ReadVarInt();
                                        VarInt mode = gameStream.ReadVarInt();
                                        Logger.Log("" + mode);
                                    }
                                }
                            }

                            


                            //Logger.Log($"Received new message ({msg.Length} bytes):\n{msg}");
                            
                        });
                        tmpThread.Start();
                    }
                    else
                    {
                        Thread.Sleep(100);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Log(e.ToString());
            }
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
