using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem;
using GemsCraft.AppSystem.Logging;
using GemsCraft.AppSystem.Types;
using GemsCraft.Network.Packets;
using GemsCraft.Players;
using Newtonsoft.Json;

namespace GemsCraft.Network
{
    internal class Protocol
    {
        public static MinecraftVersion Current = new MinecraftVersion("1.14.4", 498);

        public static void Receive(Player client, GameStream stream)
        {
            byte vi = (byte) stream.ReadVarInt().Value;
            switch (client.State)
            {
                case SessionState.Handshaking:
                    if ((Packet) vi == Packet.Handshake) HandshakePackets.ReceiveHandshake(client, stream);
                    break;
                case SessionState.Status:
                    if ((Packet) vi == Packet.Ping) StatusPackets.ReceivePing(client, stream);
                    break;
                case SessionState.Login:
                    if ((Packet) vi == Packet.LoginStart) LoginPackets.ReceiveLoginStart(client, stream);
                    if ((Packet) vi == Packet.EncryptionResponse)
                        LoginPackets.ReceiveEncryptionResponse(client, stream);
                    break;

            }
        }
    }

    public sealed class MinecraftVersion
    {
        [JsonProperty("name")]
        public string Version;

        [JsonProperty("protocol")]
        public int Protocol;

        public MinecraftVersion(string ver, int pro)
        {
            if (pro < 0) throw new ArgumentOutOfRangeException(nameof(pro));
            Version = ver ?? throw new ArgumentNullException(nameof(ver));
            Protocol = pro;
        }
    }
}
