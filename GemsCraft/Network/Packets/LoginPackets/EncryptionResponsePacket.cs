using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem;
using GemsCraft.Players;
using Newtonsoft.Json.Linq;

namespace GemsCraft.Network.Packets.LoginPackets
{
    internal class EncryptionResponsePacket : IPacket
    {
        private const string SessionCheckUri = "https://sessionserver.mojang.com/session/minecraft/hasJoined?username={0}&serverId={1}";

        public void Receive(GameStream stream, Player client)
        {
            VarInt sharedSecretLength = stream.ReadVarInt();
            byte[] sharedSecret = stream.ReadUInt8Array((int) sharedSecretLength.Value);
            VarInt verifyTokenLength = stream.ReadVarInt();
            byte[] verifyToken = stream.ReadUInt8Array((int) verifyTokenLength.Value);

            for (int i = 0; i < (int) verifyTokenLength.Value; i++)
            {
                if (verifyToken[i] != client.VerifyToken[i])
                {
                    client.Disconnect("Unable to authenticate.");
                    return;
                }
            }

            client.SharedKey = Server.CryptoServiceProvider.Decrypt(sharedSecret, false);
            AsnKeyBuilder.AsnMessage encodedKey = AsnKeyBuilder.PublicKeyToX509(Server.ServerKey);
            byte[] shaData = Encoding.UTF8.GetBytes(client.ServerId)
                .Concat(client.SharedKey)
                .Concat(encodedKey.GetBytes()).ToArray();
            string hash = Cryptography.JavaHexDigest(shaData);

            if (true) // Todo set to online mode
            {
                var webClient = new WebClient();
                var webReader = new StreamReader(webClient.OpenRead(
                    new Uri(string.Format(SessionCheckUri, client.Username, hash))));
                string response = webReader.ReadToEnd();
                webReader.Close();
                var json = JToken.Parse(response);
                if (string.IsNullOrEmpty(response))
                {
                    client.Disconnect("Failed to verify username!");
                    return;
                }

                client.UUID = json["id"].Value<string>();
            }

            client.NetworkStream = new AesStream(client.GetStream(), client.SharedKey);
            client.EncryptionEnabled = true;
            Server.LoginPlayer(stream, client);
        }

        public void Send(GameStream stream, Player client = null)
        {
            throw new NotImplementedException();
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
