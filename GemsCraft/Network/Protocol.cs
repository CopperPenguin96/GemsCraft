using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem;
using GemsCraft.Network.Packets;
using Newtonsoft.Json;

namespace GemsCraft.Network
{
    internal class Protocol
    {
        public static MinecraftVersion Current = new MinecraftVersion("1.14.4", 498);

        public static void Handle(byte id, int state, ref GameStream stream, params object[] args)
        {
            switch (state)
            {
                case 0:
                    if (id == 0x00) HandshakePackets.SendStatus(ref stream);
                    break;
            }
        }

        public static void Receive(Packet id, ref GameStream stream)
        {
            switch (stream.State)
            {
                case SessionState.Handshaking:
                    if (id == Packet.Handshake) HandshakePackets.ReceiveHandshake(ref stream);
                    if (id == Packet.Ping) HandshakePackets.ReceivePing(ref stream);
                    break;
                case SessionState.Login:
                    if (id == Packet.LoginStart) LoginPackets.ReceiveLoginStart(ref stream);
                    if (id == Packet.EncryptionResponse) LoginPackets.ReceiveEncryptionResponse(ref stream);
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
