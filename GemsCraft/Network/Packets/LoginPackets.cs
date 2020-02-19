using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using GemsCraft.AppSystem;
using GemsCraft.AppSystem.Logging;
using GemsCraft.AppSystem.Types;
using GemsCraft.Configuration;
using GemsCraft.Players;
using Newtonsoft.Json.Linq;

namespace GemsCraft.Network.Packets
{
    internal class LoginPackets
    {
        public static void ReceiveLoginStart(Player client, GameStream stream)
        {
            string username = stream.ReadString();
            stream.ServerId = RandomServerId();
            client.Username = username;
            Logger.Write(username + " is connecting.");
            
            if (Config.Security.EnableEncryption) SendEncryptionRequest(client, stream);
            else SendSuccess(client, stream);
        }

        public static void Disconnect(Player client, GameStream stream, string reason)
        {
            Protocol.Send(client, stream, Packet.LoginDisconnect, reason);
        }

        public static void SendEncryptionRequest(Player player, GameStream stream)
        {
            byte[] verifyToken = new byte[4];
            byte[] publicKey = AsnKeyBuilder.PublicKeyToX509(Server.ServerKey).GetBytes();

            var crypto = RandomNumberGenerator.Create();
            crypto.GetBytes(verifyToken);

            player.Stream.VerifyToken = verifyToken;
            Protocol.Send(player, stream, Packet.EncryptionRequest,
                new List<object>
                {
                    " ",
                    (VarInt) publicKey.Length,
                    publicKey,
                    (VarInt) verifyToken.Length,
                    verifyToken
                });
        }

        private static string RandomServerId()
        {
            var random = RandomNumberGenerator.Create();
            byte[] data = new byte[8];
            random.GetBytes(data);
            return data.Aggregate("", (current, b) => current + b.ToString("X2"));
        }

        private const string SessionChecker = "https://sessionserver.mojang.com/session/minecraft/hasJoined?username={0}&serverId={1}";

        public static void ReceiveEncryptionResponse(Player player, GameStream stream)
        {
            VarInt secretLength = stream.ReadVarInt();
            byte[] sharedSecret = stream.ReadByteArray((int)secretLength.Value);
            VarInt tokenLength = stream.ReadVarInt();
            byte[] verifyToken = stream.ReadByteArray((int)tokenLength.Value);

            var decryptedToken = Server.CryptoServerProvider.Decrypt(verifyToken, false);
            for (int i = 0; i < decryptedToken.Length; i++)
            {
                if (decryptedToken[i] != player.Stream.VerifyToken[i])
                {
                    Disconnect(player, stream, "Unable to authenticate");
                }
            }

            player.SharedToken = Server.CryptoServerProvider.Decrypt(sharedSecret, false);
            AsnKeyBuilder.AsnMessage encodedKey = AsnKeyBuilder.PublicKeyToX509(Server.ServerKey);
            byte[] shaData = Encoding.UTF8.GetBytes(Server.ID)
                .Concat(player.SharedToken)
                .Concat(encodedKey.GetBytes()).ToArray();
            string hash = Cryptography.JavaHexDigest(shaData);

            WebClient webCLient = new WebClient();
            StreamReader webReader = new StreamReader(webCLient.OpenRead(
                new Uri(string.Format(SessionChecker, player.Username, hash))
                ));
            string response = webReader.ReadToEnd();
            webReader.Close();
            JToken json = JToken.Parse(response);
            if (string.IsNullOrEmpty(response))
            {
                Disconnect(player, stream, "Failed to verify username");
            }

            player.Stream = new GameStream(new AesStream(player.Stream.BaseStream, player.SharedToken));
            player.UUID = json["id"].Value<string>();
            player.EncryptionEnabled = true;
            SendSuccess(player, player.Stream);
        }

        public static void SendSuccess(Player player, GameStream stream)
        {
            string uuid = Guid.Parse(player.UUID).ToString();
            string username = player.Username;
            Protocol.Send(player, stream, Packet.LoginSuccess,
                new List<object>
                {
                    uuid,
                    username
                });
            player.State = SessionState.Play;
            PlayPackets.JoinGame(player, stream);
            
        }
    }
}
