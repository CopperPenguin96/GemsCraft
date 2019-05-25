using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GemsCraft.AppSystem;
using GemsCraft.Configuration;
using GemsCraft.Players;
using GemsCraft.Properties;
using Newtonsoft.Json;

namespace GemsCraft.Network.Packets.StatusPackets
{
    class ResponsePacket : IPacket
    {
        public byte GetID()
        {
            return 0x00;
        }

        public SessionState GetState()
        {
            return SessionState.Status;
        }

        public void Receive(GameStream stream, Player client)
        {
            // Not implemented
        }

        public VarInt GetLength()
        {
            return 0;
        }

        public void Send(GameStream stream, Player client)
        {
            throw new NotImplementedException();
        }
        
        public void Send(GameStream stream, bool outdated)
        {
            VarInt id = GetID();
            var res = GenerateDetails(outdated);
            res.Save();
            string dets = res.GetJson();
            byte[] detsBytes = Encoding.UTF8.GetBytes(dets); // Ensure using the correct encoding UTF8
            VarInt detsLength = detsBytes.Length;
            VarInt length = id.Length + detsLength;


            stream.WriteVarInt(length + 2); // Length of the packet goes first
            stream.WriteVarInt(id); // Then the ID
            stream.WriteVarInt(detsLength); // String length must be sent before the string :|
            stream.WriteUInt8Array(detsBytes); // Sending the string
        }

        public void SendLegacy(GameStream stream, bool outdated)
        {
            // TODO - implement legacy status
        }

        private static ResponseData GenerateDetails(bool outdated)
        {
            return new ResponseData
            {
                Version = Protocol.MCVersion,
                Players = new PlayerList(),
                Icon = "data:image/png;base64," + new ServerIcon(Resources.server_icon)
            };

        }
    }
    

    public class ResponseData
    {
        [JsonProperty("description")]
        public NetworkText Description = new NetworkText { text = "Welcome!" };

        [JsonProperty("players")]
        public PlayerList Players { get; set; }

        [JsonProperty("version")]
        public MinecraftVersion Version { get; set; }

        [JsonProperty("favicon")]
        public string Icon { get; set; }

        public string GetJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public override string ToString()
        {
            return GetJson();
        }

        public void Save()
        {
            var writer = File.CreateText("json.txt");
            writer.WriteLine(GetJson());
            writer.Flush();
            writer.Close();
        }
    }

    public struct NetworkText
    {
        public string text;
    }
    

    public class PlayerList
    {
        public class SamplePlayer
        {
            public SamplePlayer()
            {

            }

            public SamplePlayer(string name, string id)
            {
                Name = name;
                Id = id;
            }

            [JsonProperty("name")]
            public string Name { get; set; }
            
            [JsonProperty("id")]
            public string Id { get; set; }
        }
        
        public PlayerList()
        {
            MaxPlayers = Server.Config.MaxPlayers;
            OnlinePlayers = 1;
            SamplePlayer p = new SamplePlayer
            {
                Name = "alexpotter96",
                Id = Player.Console.UUID
            };
            Players = new SamplePlayer[] { };
        }

        [JsonProperty("max")]
        public int MaxPlayers { get; set; }
        [JsonProperty("online")]
        public int OnlinePlayers { get; set; }
        [JsonProperty("sample")]
        public SamplePlayer[] Players { get; set; }
    }


}
