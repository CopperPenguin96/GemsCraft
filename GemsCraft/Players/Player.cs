using System;
using System.IO;
using GemsCraft.AppSystem;
using GemsCraft.Network;
using Newtonsoft.Json;

namespace GemsCraft.Players
{
    public class Player
    {
        [JsonProperty("Username")]
        public string Username { get; }
        [JsonProperty("UUID")]
        public string UUID { get; }

        internal GameStream Stream;

        public Player(string username, string uuid)
        {
            Username = username;
            UUID = uuid;
        }

        public void SetStream(ref GameStream stream)
        {
            Stream = stream;
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
