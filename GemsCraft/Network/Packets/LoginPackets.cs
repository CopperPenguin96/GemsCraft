using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem;
using GemsCraft.AppSystem.Logging;
using GemsCraft.AppSystem.Types;
using GemsCraft.Players;
using GemsCraft.Utils;
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
            SendEncryptionRequest(client, stream);
        }

        public static void Disconnect(string reason, GameStream stream)
        {
            VarInt id = (byte)Packet.LoginDisconnect;
            VarInt strLength = reason.Length;
            VarInt length = id.Length + reason.Length + strLength.Length;
            stream.WriteVarInt(length);
            stream.WriteVarInt(id);
            stream.WriteString(reason);
        }

        public static void SendEncryptionRequest(Player player, GameStream stream)
        {
            VarInt pck = (VarInt)(byte)Packet.EncryptionRequest;
            byte[] verifyToken = new byte[4];
            byte[] publicKey = AsnKeyBuilder.PublicKeyToX509(Server.ServerKey).GetBytes();

            var crypto = RandomNumberGenerator.Create();
            crypto.GetBytes(verifyToken);

            player.Stream.VerifyToken = verifyToken;

            VarInt vT = verifyToken.Length;
            VarInt pk = publicKey.Length;

            // IMPORTANT   ID Length    String ID Length   Verify Token     Public Key
            VarInt total = pck.Length + Server.ID.Length + vT + vT.Length + pk + pk.Length + 1;

            player.Stream.WriteVarInt(total);
            player.Stream.WriteVarInt(pck);
            player.Stream.WriteString(Server.ID);

            player.Stream.WriteVarInt(publicKey.Length);
            player.Stream.WriteUInt8Array(publicKey);

            player.VerifyToken = verifyToken;
            player.Stream.WriteVarInt(verifyToken.Length);
            player.Stream.WriteUInt8Array(verifyToken);
        }

        private static string RandomServerId()
        {
            var random = RandomNumberGenerator.Create();
            byte[] data = new byte[8];
            random.GetBytes(data);
            return data.Aggregate("", (current, b) => current + b.ToString("X2"));
        }

        private const string sessionChecker = "https://sessionserver.mojang.com/session/minecraft/hasJoined?username={0}&serverId={1}";

        public static void ReceiveEncryptionResponse(Player player, GameStream stream)
        {
            VarInt secretLength = stream.ReadVarInt();
            byte[] sharedSecret = stream.ReadByteArray((int)secretLength.Value);
            VarInt tokenLength = stream.ReadVarInt();
            byte[] verifyToken = stream.ReadByteArray((int)tokenLength.Value);

            var decryptedToken = Server.CryptoServerProvider.Decrypt(verifyToken, false);
            for (int i = 0; i < decryptedToken.Length; i++)
            {
                if (decryptedToken[i] != player.VerifyToken[i])
                {
                    Disconnect("Unable to authenticate", stream);
                }
            }

            player.SharedToken = Server.CryptoServerProvider.Decrypt(sharedSecret, false);
            AsnKeyBuilder.AsnMessage encodedKey = AsnKeyBuilder.PublicKeyToX509(Server.ServerKey);
            byte[] shaData = Encoding.UTF8.GetBytes(Server.ID)
                .Concat(player.SharedToken)
                .Concat(encodedKey.GetBytes()).ToArray();
            string hash = Cryptography.JavaHexDigest(shaData);

            if (true) // Server is online mode, update with config
            {
                WebClient webCLient = new WebClient();
                StreamReader webReader = new StreamReader(webCLient.OpenRead(
                    new Uri(string.Format(sessionChecker, player.Username, hash))
                    ));
                string response = webReader.ReadToEnd();
                webReader.Close();
                JToken json = JToken.Parse(response);
                if (string.IsNullOrEmpty(response))
                {
                    Disconnect("Failed to verify username!", stream);
                }

                player.UUID = json["id"].Value<string>();
                player.EncryptionEnabled = true;
                SendSuccess(player, stream);
            }
        }

        public static void SendSuccess(Player player, GameStream stream)
        {
            VarInt id = (VarInt)(byte)Packet.LoginSuccess;
            string uuid = Guid.Parse(player.UUID).ToString();
            string username = player.Username;
            VarInt uLength = uuid.Length;
            VarInt usLength = username.Length;
            VarInt length = uLength.Length + usLength.Length + 
                            id.Length + uuid.Length + username.Length;

            player.Stream.WriteVarInt(length);
            player.Stream.WriteVarInt(id);
            player.Stream.WriteString(uuid);
            player.Stream.WriteString(username);
            player.State = SessionState.Play;
        }
    }
}
