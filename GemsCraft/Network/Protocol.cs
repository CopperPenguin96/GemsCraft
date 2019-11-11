using System;
using System.Collections.Generic;
using System.Text;
using GemsCraft.AppSystem.Types;
using GemsCraft.Network.Packets;
using GemsCraft.Players;
using Newtonsoft.Json;

namespace GemsCraft.Network
{
    internal class Protocol
    {
        public static MinecraftVersion Current = new MinecraftVersion("1.13.2", 404); // Going back to 404 for now

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

        public static void Send(Player client, GameStream stream,
            Packet packet, object content)
        {
            if (content is string str)
            {
                byte[] bts = Encoding.UTF8.GetBytes(str);
                Send(client, stream, packet, new List<object> {(VarInt) bts.Length, bts});
            }
            else
            {
                Send(client, stream, packet, new List<object> {content});
            }
        }

        public static void Send(Player client, GameStream stream,
            Packet packet, List<object> content)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (stream == null) throw new ArgumentNullException(nameof(stream));
            if (content == null) throw new ArgumentNullException(nameof(content));
            if (packet == Packet.Unknown)
            {
                throw new PacketException("Unknown packet: " + (byte) packet);
            }

            
            VarInt id = (VarInt) (byte) packet;
            VarInt length = id.Length;
            foreach (object o in content)
            {
                length += GetItemLength(o);
            }
            
            client.Stream.WriteVarInt(length);
            client.Stream.WriteVarInt(id);

            foreach (object o in content)
            {
                client.Stream.Write(o);
            }
            
            client.Stream.Flush();
        }

        private static VarInt GetItemLength(object o)
        {
            if (o == null) throw new ArgumentNullException(nameof(o));
            switch (o)
            {
                case bool _:
                case sbyte _:
                case byte _:
                    return 1;
                case short _:
                case ushort _:
                    return 2;
                case int _:
                case float _:
                    return 4;
                case long _:
                case double _:
                    return 8;
                case string str:
                    return str.Length;
                case VarInt vI:
                    return vI.Length;
                case byte[] b:
                    return b.Length;
            }

            throw new PacketException("Type not supported");
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
