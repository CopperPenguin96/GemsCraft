using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem.Types;
using GemsCraft.Configuration;
using GemsCraft.Players;
using GemsCraft.Properties;
using GemsCraft.Utils;

namespace GemsCraft.Network.Packets
{
    internal class StatusPackets
    {
        public static void SendStatus(Player client)
        {
            GameStream stream = new GameStream(client.Client.GetStream());
            int id = (byte)Packet.Response;
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
            byte[] detsBytes = Encoding.UTF8.GetBytes(details);
            VarInt detsLength = detsBytes.Length;
            VarInt idLength = ((VarInt)id).Length + detsLength.Length;
            stream.WriteVarInt(idLength + detsLength);
            stream.WriteVarInt(id);
            stream.WriteVarInt(detsLength);
            stream.WriteUInt8Array(detsBytes);
            stream.Flush();
        }

        public static void ReceivePing(Player player, GameStream stream)
        {
            long payload = stream.ReadInt64();
            stream.Payload = payload;
            SendPong(player);
        }

        public static void SendPong(Player player)
        {
            GameStream stream = new GameStream(player.Client.GetStream());
            VarInt id = (byte)Packet.Pong;
            VarInt length = id.Length + 8; // 8 for Long length
            stream.WriteVarInt(length);
            stream.WriteVarInt(id);
            stream.WriteInt64(stream.Payload);
        }
    }
}
