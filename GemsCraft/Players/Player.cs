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
        public TcpClient Client;
        public SessionState State;
        [JsonProperty("Username")]
        public string Username { get; internal set; }
        [JsonProperty("UUID")]
        public string UUID { get; }

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
