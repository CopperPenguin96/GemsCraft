using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem;
using GemsCraft.Players;

namespace GemsCraft.Network.Packets.LoginPackets
{
    internal class EncryptionRequestPacket : IPacket
    {
        public void Receive(GameStream stream, Player client)
        {
            throw new NotImplementedException();
        }

        private static readonly string base_url =
            "https://sessionserver.mojang.com/session/minecraft/hasJoined";
        public void Send(GameStream stream, Player client)
        {
            VarInt packetId = GetID();

            byte[] serverId = Encoding.UTF8.GetBytes(CreateID());
            VarInt serverIdLength = serverId.Length;
            var encodedKey = AsnKeyBuilder.PublicKeyToX509(Server.ServerKey);
            VarInt encodedKeylength = encodedKey.Length;
            VarInt verifyTokenLength = 4;
            var verifyToken = new byte[4];
            var csp = new RNGCryptoServiceProvider();
            csp.GetBytes(verifyToken);
            client.VerifyToken = verifyToken;

            VarInt packetLength = packetId.Length + serverIdLength + serverIdLength.Length +
                                  encodedKeylength + encodedKeylength.Length +
                                  verifyTokenLength + verifyTokenLength.Length;
            stream.WriteVarInt(packetLength); // Packet Length
            stream.WriteVarInt(packetId); // Packet Id (0x01)

            stream.WriteVarInt(serverIdLength); // Length of string
            stream.WriteUInt8Array(serverId); // Then string
            stream.WriteVarInt(encodedKeylength);
            stream.WriteUInt8Array(encodedKey.GetBytes());
            stream.WriteVarInt(verifyTokenLength);
            stream.WriteUInt8Array(verifyToken);

            stream.Flush();
        }

        private static string CreateID()
        {
            var random = RandomNumberGenerator.Create();
            byte[] data = new byte[8];
            random.GetBytes(data);
            return data.Aggregate("", (current, b) => current + b.ToString("X2"));
        }

        public byte GetID()
        {
            return 0x01;
        }

        public VarInt GetLength()
        {
            throw new NotImplementedException();
        }

        public SessionState GetState()
        {
            throw new NotImplementedException();
        }
    }
}
