using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem;

namespace GemsCraft.Network.Packets.LoginPackets
{
    internal class EncryptionRequestPacket : IPacket
    {
        public void Receive(GameStream stream)
        {
            throw new NotImplementedException();
        }

        public void Send(GameStream stream)
        {
            string serverId;
            VarInt publicKeyLength;
            byte[] publicKey;
            VarInt verifyTokenLength;
            byte[] verifyToken;
            
            // Write Total Length
            // Write ID
            // Write Data
            // Flush()
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
