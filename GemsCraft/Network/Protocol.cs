using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem;
using GemsCraft.Configuration;
using GemsCraft.Network.Packets;
using GemsCraft.Network.Packets.StatusPackets;
using GemsCraft.Players;
using Newtonsoft.Json;

namespace GemsCraft.Network
{
    internal class Protocol
    {
        public static readonly MinecraftVersion MCVersion = new MinecraftVersion("1.13.2", 404);
        
        public static string Handshake(GameStream stream, VarInt protVer, string ip, ushort port, VarInt nextState)
        {
            int Version = MCVersion.Protocol;
            if (protVer.Value > Version) // Check to see if client is on new version
            {
                return "Server is still on Minecraft Version " + MCVersion;
            }
            else if (protVer.Value < Version) // Check to see if client in on older version
            {
                return "Outdated Client.";
            }
            else // If both client and server match, go for it!
            {
                if (IPAddress.TryParse(ip + ":" + port, out _))
                {
                    return "Invalid IP Address";
                }

                NextState state = (NextState) nextState.Value;
                switch (state)
                {
                    case NextState.Status:
                        return "continue";
                    case NextState.Login:
                        // TODO handle
                        return null;
                    default:
                        throw new Exception("Unrecognized NextState");
                }
            }
        }

        //TODO - Add packets here as they come along
        public static ResponsePacket ResponsePacket = new ResponsePacket();
    }

    public struct MinecraftVersion
    {
        [JsonProperty("name")]
        public string Name;
        [JsonProperty("protocol")]
        public int Protocol;

        public MinecraftVersion(string name, int pro)
        {
            this.Name = name;
            this.Protocol = pro;
        }
    }
    public enum NextState
    {
        Status = 1,
        Login = 2
    }
}
