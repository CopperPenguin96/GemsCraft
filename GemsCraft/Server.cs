using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GemsCraft.AppSystem;
using GemsCraft.AppSystem.Types;
using GemsCraft.Network;
using GemsCraft.Network.Packets;
using GemsCraft.Players;

namespace GemsCraft
{
    public enum SessionState { Handshaking, Login, Status, Play }
    public class Server
    {
        public static PlayerList OnlinePlayers = new PlayerList();

        protected internal static RSACryptoServiceProvider CryptoService { get; set; }
        protected internal static RSAParameters Key { get; set; }
        public static void Start()
        {
            Logger.Write("GemsCraft is starting...");
            Files.CheckPaths();
            Thread serverThread = new Thread(Run);
            serverThread.Start();
        }

        private static void Run()
        {
            IPAddress ip = IPAddress.Parse("0.0.0.0");
            int port = 25565;
            TcpListener server = new TcpListener(ip, port);
            
            Logger.Write("Starting the server on " + ip + ":" + port);
            CryptoService = new RSACryptoServiceProvider();
            Key = CryptoService.ExportParameters(true);
            server.Start();

            try
            {
                while (true)
                {
                    if (!server.Pending()) continue;
                    Thread tmpThread = new Thread(() =>
                    {
                        TcpClient client = server.AcceptTcpClient();
                        
                        using (NetworkStream ns = client.GetStream())
                        {
                            GameStream stream = new GameStream(ns) {State = SessionState.Handshaking};
                            using (StreamReader sr = new StreamReader(stream))
                            {
                                while (true)
                                {
                                    VarInt length = stream.ReadVarInt();
                                    byte id = (byte) stream.ReadVarInt().Value;
                                    Protocol.Receive((Packet) id, ref stream);
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
    }
}
