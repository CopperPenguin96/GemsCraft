using System.Collections.Generic;
using System.IO;
using System.Text;
using GemsCraft.AppSystem.Types;
using GemsCraft.Configuration;
using GemsCraft.Players;
using GemsCraft.Utils;

namespace GemsCraft.Network.Packets
{
    internal class StatusPackets
    {
        public static void SendStatus(Player client, GameStream stream)
        {
            ResponseData data = new ResponseData
            {
                Version = Protocol.Current,
                Description = new NetworkText
                {
                    text = Config.Current.MOTD
                },
                Players = new PlayerStatusList(),
                Icon = "data:image/png;base64," + Config.Current.Icon
            };

            data.Save(); // Save for debug purposes
            string details = data.GetJson();
            Protocol.Send(client, stream, Packet.Response, details);
        }

        public static void ReceivePing(Player player, GameStream stream)
        {
            long payload = stream.ReadLong();
            stream.Payload = payload;
            SendPong(player, stream);
        }

        public static void SendPong(Player player, GameStream stream)
        {
            Protocol.Send(player, stream, Packet.Pong, stream.Payload);
        }
    }
}
