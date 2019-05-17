using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Claims;
using System.Threading;
using System.Windows.Forms;
using GemsCraft.AppSystem;
using GemsCraft.Configuration;
using GemsCraft.Network;
using GemsCraft.Network.Packets;
using GemsCraft.Network.Packets.StatusPackets;
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
                TcpListener listener = new TcpListener(ip, 12948);
                listener.Start();
                Logger.Log(LogType.ServerStartup, "Now accepting connections.");
                while (true)
                {
                    if (!listener.Pending()) continue;
                    Thread tmpThread = new Thread(() =>
                    {
                        
                        TcpClient client = listener.AcceptTcpClient();
                        SessionState state = SessionState.Handshaking;
                        using (NetworkStream ns = client.GetStream())
                        {
                            GameStream gameStream = new GameStream(ns);
                            using (StreamReader sr = new StreamReader(gameStream))
                            {

                                while (true)
                                {
                                    VarInt length = gameStream.ReadVarInt(); // Always read the length first
                                    VarInt id = gameStream.ReadVarInt();
                                    state = DataMethod(state, id, gameStream);
                                }
                            }
                        }
                    });
                    tmpThread.Start();
                }
            }
            catch (Exception e)
            {
                Logger.Log(e.ToString());
            }
        }

        /// <summary>
        /// Handle all packets to and from
        /// </summary>
        private static SessionState DataMethod(SessionState currentState, VarInt vid, GameStream gameStream)
        {
            byte id = (byte) vid.Value;
            bool outdated = false;
            switch (currentState)
            { 
                case SessionState.Handshaking:
                    {
                        if (id == 0x00)
                        {
                            VarInt pro = gameStream.ReadVarInt();
                            string address = gameStream.ReadString();
                            ushort port = gameStream.ReadUInt16();
                            VarInt state = gameStream.ReadVarInt();
                            string response = Protocol.Handshake(
                                gameStream, // Stream to allow continuing other packets
                                pro, // Minecraft version protocol
                                address, // Server's IP Address used to connect
                                port, // Port used to connect
                                state); // State to continue to (1 for Server Status [On the server list] or 2 to login)
                            switch (response)
                            {
                                case "continue":
                                    return SessionState.Status;
                                case "Outdated Client":
                                    return SessionState.Status;
                            }
                        }

                        break;
                    }
                case SessionState.Status:
                    {
                        if (id == 0x00) // Request Packet
                        {
                            Protocol.ResponsePacket.Send(gameStream, outdated); // Response with Response Packet
                        }
                        else if (id == 0x01) // Ping Packet
                        {
                            long payload = gameStream.ReadInt64(); // ping
                            VarInt pckId = 0x01;
                            VarInt length = pckId.Length + DataLength.Long;
                            gameStream.WriteVarInt(length);
                            gameStream.WriteVarInt(pckId);
                            gameStream.WriteInt64(payload); // pong
                        }
                        break;
                    }
            }

            return currentState;
        }

        private static bool firstAttemptAtPing = true;
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
