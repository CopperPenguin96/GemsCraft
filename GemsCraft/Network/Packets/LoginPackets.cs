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
using GemsCraft.AppSystem.Types;
using GemsCraft.Players;
using GemsCraft.Utils;
using Newtonsoft.Json.Linq;

namespace GemsCraft.Network.Packets
{
    internal class LoginPackets
    {
        public static void ReceiveLoginStart(ref GameStream stream)
        {
            string username = stream.ReadString(); // TODO - set up player object
            PlayerDB.LoadPlayer(username, ref stream, out Player player);
            stream.Username = username;
            Logger.Write(username + " is connecting.");
            SendEncryptionRequest(ref player);
        }

        public static void Disconnect(string reason, ref GameStream stream)
        {
            VarInt id = (byte) Packet.LoginDisconnect;
            VarInt strLength = reason.Length;
            VarInt length = id.Length + reason.Length + strLength.Length;
            stream.WriteVarInt(length);
            stream.WriteVarInt(id);
            stream.WriteString(reason);
        }

        public static void SendEncryptionRequest(ref Player player)
        {
            using (var rsa = new RSACryptoServiceProvider(1024))
            {
                try
                {
                    var cryptoServiceProvider = new RSACryptoServiceProvider(1024);
                    var serverKey = cryptoServiceProvider.ExportParameters(true);
                    byte[] verificationToken = new byte[4];
                    var provider = new RNGCryptoServiceProvider();
                    provider.GetBytes(verificationToken);
                    player.Stream.VerifyToken = verificationToken;

                    var encoded = AsnKeyBuilder.PublicKeyToX509(serverKey).GetBytes();

                    string serverID = "";
                    // id, public key and length, verify and length
                    VarInt id = (byte) Packet.EncryptionRequest;
                    VarInt idLength = id.Length;
                    VarInt encodedLength = encoded.Length;
                    VarInt tokLength = player.Stream.VerifyToken.Length;
                    VarInt length = idLength + serverID.Length +
                                    encoded.Length + encodedLength.Length +
                                    player.Stream.VerifyToken.Length + tokLength.Length;
                    player.Stream.WriteVarInt(length);
                    player.Stream.WriteVarInt(id);
                    player.Stream.WriteString(serverID);
                    player.Stream.WriteVarInt(encodedLength);
                    player.Stream.WriteUInt8Array(encoded);
                    player.Stream.WriteVarInt(tokLength);
                    player.Stream.WriteUInt8Array(player.Stream.VerifyToken);
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }

        private const string sessionChecker = "https://sessionserver.mojang.com/session/minecraft/hasJoined?username={0}&serverId={1}";



        public static void ReceiveEncryptionResponse(ref GameStream stream)
        {
            VarInt sharedLength = stream.ReadVarInt();
            byte[] sharedSecret = stream.ReadUInt8Array((int)sharedLength.Value);
            VarInt verifyLength = stream.ReadVarInt();
            byte[] verifyToken = stream.ReadUInt8Array((int) verifyLength.Value);
            var decrypto = Server.CryptoService.Decrypt(verifyToken, false);
            for (int i = 0; i < decrypto.Length; i++)
            {
                if (decrypto[i] == stream.VerifyToken[i]) continue;
                Disconnect("Unable to authenticate. :'(", ref stream);
                return;
            }

            stream.SharedKey = Server.CryptoService.Decrypt(sharedSecret, false);
            AsnKeyBuilder.AsnMessage encodedKey = AsnKeyBuilder.PublicKeyToX509(Server.Key);
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
                Disconnect("Failed to verify username!", ref stream);
                return;
            }
            
        }
    }
}
