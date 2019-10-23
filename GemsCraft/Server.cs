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
using GemsCraft.AppSystem.Logging;
using GemsCraft.AppSystem.Types;
using GemsCraft.Network;
using GemsCraft.Network.Packets;
using GemsCraft.Players;
using GemsCraft.Utils;

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
            Files.CheckPaths(); // Ensures all paths are ready for the server to start
            Logger.AddToFull("-------------Started Session on " +
                             DateTime.Now.ToLongDateString() + " @ " + DateTime.Now.ToLongTimeString() +
                             "-------------", true);
            Logger.Write("GemsCraft is starting...");
            CryptoServerProvider = new RSACryptoServiceProvider(1024);
            ServerKey = CryptoServerProvider.ExportParameters(true);

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
    }
}
