using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading;
using System.Windows.Forms;
using GemsCraft.AppSystem;
using GemsCraft.Configuration;
using GemsCraft.Network;
using GemsCraft.Network.Packets;
using GemsCraft.Network.Packets.StatusPackets;
using GemsCraft.Players;
using JetBrains.Annotations;

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
            Player.Console = Player.CreateInstance("Console");
            // Server thread
            Thread serverThread = new Thread(ServerThread);
            serverThread.Start();
        }

        protected internal static RSACryptoServiceProvider CryptoServiceProvider { get; set; }
        protected internal static RSAParameters ServerKey { get; set; }
        
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
                CryptoServiceProvider = new RSACryptoServiceProvider(1024);
                ServerKey = CryptoServiceProvider.ExportParameters(true);
                
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
                                    state = HandleData(state, id, gameStream, (Player) client);
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
        private static SessionState HandleData(SessionState currentState, VarInt vid, GameStream gameStream, Player client)
        {
            byte id = (byte) vid.Value;
            bool outdated = false;
            switch (currentState)
            { 
                case SessionState.Handshaking:
                    return HandshakingState(gameStream, vid);
                case SessionState.Status:
                    return StatusState(gameStream, vid, outdated);
                case SessionState.Login:
                    return LoginState(gameStream, vid, client);
            }

            return currentState;
        }

        private static SessionState HandshakingState(GameStream gameStream, VarInt id)
        {
            if (id == 0x00)
            {
                VarInt pro = gameStream.ReadVarInt();
                string address = gameStream.ReadString();
                ushort port = gameStream.ReadUInt16();
                VarInt state = gameStream.ReadVarInt();
                ProtocolResponse response = Protocol.Handshake(
                    gameStream, // Stream to allow continuing other packets
                    pro, // Minecraft version protocol
                    address, // Server's IP Address used to connect
                    port, // Port used to connect
                    state); // State to continue to (1 for Server Status [On the server list] or 2 to login)

                switch (response)
                {
                    case ProtocolResponse.InvalidInternetAddress:
                        Logger.Log(LogType.Error, "Unable to start server. Invalid IP Address");
                        Console.ReadLine();
                        break;
                    default:
                        return state == 1 ? SessionState.Status : SessionState.Login;
                      
                }

                return 0;
            }
            else throw new Exception("Invalid ID for handshaking...");
        }

        private static SessionState StatusState(GameStream gameStream, VarInt vid, bool outdated)
        {
            byte id = (byte) vid.Value;
            if (id == 0x00) // Request Packet
            {
                Protocol.ResponsePacket.Send(gameStream, outdated); // Response with Response Packet
                return SessionState.Status;
            }
            else if (id == 0x01) // Ping Packet
            {
                long payload = gameStream.ReadInt64(); // ping
                VarInt pckId = 0x01;
                VarInt length = pckId.Length + DataLength.Long;
                gameStream.WriteVarInt(length);
                gameStream.WriteVarInt(pckId);
                gameStream.WriteInt64(payload); // pong
                return SessionState.Status;
            }
            else
            {
                throw new Exception("Invalid Packet for Status State");
            }
        }

        private static SessionState LoginState(GameStream gameStream, VarInt id, Player client)
        {
            if (id == 0x00)
            {
                Protocol.LoginStartPacket.Receive(gameStream, client); // Get username
            }
            else if (id == 0x01)
            {
                Protocol.EncryptionResponsePacket.Receive(gameStream, client); // Get encryption response from client
            }

            while (!client.LoginCompleted)
            {
                
            }

            return SessionState.Play;
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

        public static void Message(Player player, string message)
        {
            Player.Console.Message(player, message);
            foreach (Player p in OnlinePlayers)
            {
                p.Message(player, message);
            }
        }
 
        [StringFormatMethod("message")]
        public static void Message(Player player, string message, params object[] formatArgs)
        {
            Message(player, string.Format(message, formatArgs));
        }

        public static void LoginPlayer(GameStream stream, Player player)
        {
            Protocol.LoginSuccessPacket.Send(stream, player);
        }
    }
}
