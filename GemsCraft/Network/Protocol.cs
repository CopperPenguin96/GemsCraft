using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using GemsCraft.AppSystem;
using GemsCraft.Configuration;
using GemsCraft.Network.Packets;
using GemsCraft.Network.Packets.LoginPackets;
using GemsCraft.Network.Packets.StatusPackets;
using GemsCraft.Players;
using Newtonsoft.Json;

namespace GemsCraft.Network
{
    internal class Protocol
    {
        public static readonly MinecraftVersion MCVersion = new MinecraftVersion("1.13.2", 404);
        
        public static ProtocolResponse Handshake(GameStream stream, VarInt protVer, string ip, ushort port, VarInt nextState)
        {
            int version = MCVersion.Protocol;
            if (protVer.Value > version) // Check to see if client is on new version
            {
                return ProtocolResponse.OutdatedServer;
            }

            if (protVer.Value < version) // Check to see if client in on older version
            {
                return ProtocolResponse.OutdatedClient;
            }

            return IPAddress.TryParse(ip + ":" + port, out _) ? ProtocolResponse.InvalidInternetAddress : ProtocolResponse.Updated;
        }

        public static bool LoginStart(string username)
        {
            List<string> errors = new List<string>();
            if (!CheckUsername(username, out errors))
            {
                foreach (string err in errors)
                {
                    Logger.Log(LogType.Error,
                        $"There was an error processing this username {username}: {err}");
                }

                return false;
            }

            bool foundOne = false;
            foreach (Player p in Server.OnlinePlayers)
            {
                // ReSharper disable once InvertIf
                if (p.Username == username)
                {
                    foundOne = true;
                    Server.Message(Player.Console, "{0} has returned!", username);
                }
            }

            if (!foundOne)
            {
                Server.Message(Player.Console, "Welcome {0} to the server!", username);
            }

            return true;
        }
        
        /// <summary>
        /// Checks the provided username for errors
        /// </summary>
        /// <param name="username">The username supplied</param>
        /// <param name="errors">If there are errors, they will be present here</param>
        /// <returns>True if there are no errors. False if there are errors</returns>
        public static bool CheckUsername(string username, out List<string> errors)
        {
            List<string> subErrors = new List<string>();

            if (username.Length < 3) subErrors.Add("Username must be within 3-16 characters.");
            if (username.Length > 16) subErrors.Add("Username must be within 3-16 characters");
            if (username.Contains(" ")) subErrors.Add("Username cannot contain spaces.");
            string[] allowed =
            {
                "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p",
                "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "0", "1", "2", "3", "4", "5",
                "6", "7", "8", "9", "_"
            };

            subErrors.AddRange(from c in username.ToCharArray()
                let cLower = ("" + c).ToLower()
                where !allowed.Contains(cLower)
                select c + " is not a valid character for a username.");
            errors = subErrors;
            return subErrors.Count <= 0;
        }

        //TODO - Add packets here as they come along

        // Status Packets
        public static ResponsePacket ResponsePacket = new ResponsePacket(); // Client Bound
        
        // Login Packets
        public static LoginStartPacket LoginStartPacket =  new LoginStartPacket(); // Serverbound
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

    public enum ProtocolResponse
    { 
        Updated, OutdatedClient, OutdatedServer, InvalidInternetAddress
    }
}
