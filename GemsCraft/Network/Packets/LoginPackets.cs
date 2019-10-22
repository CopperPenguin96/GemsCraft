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
        public static void ReceiveLoginStart(GameStream stream)
        {
            string username = stream.ReadString();
            stream.ServerId = RandomServerId();
            PlayerDB.LoadPlayer(username, stream, out Player player);
            stream.Username = username;
            Logger.Write(username + " is connecting.");
            SendEncryptionRequest(player);
            Disconnect("Wut", stream);
        }

        public static void Disconnect(string reason, GameStream stream)
        {
            VarInt id = (byte) Packet.LoginDisconnect;
            VarInt strLength = reason.Length;
            VarInt length = id.Length + reason.Length + strLength.Length;
            stream.WriteVarInt(length);
            stream.WriteVarInt(id);
            stream.WriteString(reason);
        }

        public static void SendEncryptionRequest(Player player)
        {
            byte[] verifyToken = new byte[4];
            var crypto = new RNGCryptoServiceProvider();
            crypto.GetBytes(verifyToken);
            player.Stream.VerifyToken = verifyToken;
            string serverid = RandomServerId(); // According to Protocol it appears to be empty, so let's leave it empty :/
            var encodedKey = Convert.ToBase64String(Server.PublicKey).ToBytes();

            VarInt id = (byte) Packet.EncryptionRequest; // Packet ID
            VarInt strLength = serverid.Length; // Length of the server id 0.0
            VarInt strLengthLength = strLength.Length;
            VarInt publicKeyLength = encodedKey.Length; // 
            VarInt verLength = verifyToken.Length;
            VarInt total = strLength + publicKeyLength + verLength + strLength;
                               
            /*
             * Lengths needed to be sent
             * ID,
             * ServerID,
             * publicKeyLength
             * publicKey
             * verifyTokenLength
             * verifyToken
             */

            player.Stream.WriteVarInt(total); 
            player.Stream.WriteVarInt(id); 
            player.Stream.WriteString("");
            player.Stream.WriteVarInt(encodedKey.Length);
            player.Stream.WriteUInt8Array(encodedKey);
            player.Stream.WriteVarInt(verifyToken.Length);
            player.Stream.WriteUInt8Array(verifyToken);
            player.Stream.Flush();
        }

        private static string RandomServerId()
        {
            var random = RandomNumberGenerator.Create();
            byte[] data = new byte[8];
            random.GetBytes(data);
            return data.Aggregate("", (current, b) => current + b.ToString("X2"));
        }

        private const string sessionChecker = "https://sessionserver.mojang.com/session/minecraft/hasJoined?username={0}&serverId={1}";
        
        public static void ReceiveEncryptionResponse(GameStream stream)
        {
            VarInt sharedLength = stream.ReadVarInt();
            byte[] sharedSecret = stream.ReadUInt8Array((int)sharedLength.Value);
            VarInt verifyLength = stream.ReadVarInt();
            byte[] verifyToken = stream.ReadUInt8Array((int) verifyLength.Value);
            var decrypto = new byte[5];
            for (int i = 0; i < decrypto.Length; i++)
            {
                if (decrypto[i] == stream.VerifyToken[i]) continue;
                Disconnect("Unable to authenticate. :'(", stream);
                return;
            }

            stream.SharedKey = Server.Crypto.Decrypt(sharedSecret, false);
            AsnKeyBuilder.AsnMessage encodedKey = AsnKeyBuilder.PublicKeyToX509(Server.Crypto.ExportParameters(false));
            byte[] shaData = Encoding.UTF8.GetBytes("")
                .Concat(stream.SharedKey)
                .Concat(encodedKey.GetBytes()).ToArray();
            string hash = Cryptography.JavaHexDigest(shaData);

            // Let's have a little chat with Mojang
            WebClient cl = new WebClient();
            StreamReader reader = new StreamReader(cl.OpenRead(
                new Uri(string.Format(sessionChecker, stream.Username, hash)))); 
            string response = reader.ReadToEnd();
            reader.Close();
            var json = JToken.Parse(response);
            if (string.IsNullOrEmpty(response))
            {
                Disconnect("Failed to verify username!", stream);
                return;
            }
            else
            {
                Disconnect(response, stream);
            }
            
        }
    }
}
