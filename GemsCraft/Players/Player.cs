using System;
using System.IO;
using System.Net.Sockets;
using GemsCraft.AppSystem;
using GemsCraft.AppSystem.Logging;
using GemsCraft.Network;
using Newtonsoft.Json;

namespace GemsCraft.Players
{
    public class Player
    {
        /// <summary>
        /// The Minecraft Client
        /// </summary>
        internal TcpClient Client;
        /// <summary>
        /// Used to identify the player
        /// </summary>
        internal int Eid;
        /// <summary>
        /// Used for encryption
        /// </summary>
        internal AesStream AesStream;
        /// <summary>
        /// Used by the server to determine which state it is at when
        /// communicating with the client
        /// </summary>
        public SessionState State;

        /// <summary>
        /// Used during Encryption
        /// </summary>
        protected internal byte[] VerifyToken;

        /// <summary>
        /// Used during Encryption
        /// </summary>
        protected internal byte[] SharedToken;

        /// <summary>
        /// Will be set to true if encryption succeeds
        /// </summary>
        internal bool EncryptionEnabled = false;

        /// <summary>
        /// The unchanging username of the player
        /// </summary>
        [JsonProperty("Username")]
        public string Username { get; internal set; }

        /// <summary>
        /// The fixed identifier of the player, used to id them
        /// rather than their username in case the player changes
        /// their username
        /// </summary>
        [JsonProperty("UUID")]
        public string UUID { get; internal set; }

        /// <summary>
        /// Used by the server and client to communicate
        /// </summary>
        internal GameStream Stream;

        public Player(TcpClient client)
        {
            Client = client;
            Stream = new GameStream(client.GetStream());
        }

        public Player(string username, string uuid)
        {
            Username = username;
            UUID = uuid;
        }

        public void Save()
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            var writer = File.CreateText(Files.PlayerDatabasePath + UUID + ".json");
            writer.Write(json);
            writer.Flush();
            writer.Close();
        }

        public void TrySave()
        {
            try
            {
                Save();
            }
            catch (Exception e)
            {
                Logger.Write(e.ToString());
            }
        }
    }
}
