using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem;
using GemsCraft.Players;

namespace GemsCraft.Network.Packets.LoginPackets
{
    internal class LoginSuccessPacket : IPacket
    {
        public void Receive(GameStream stream, Player client)
        {
            throw new NotImplementedException();
        }

        public void Send(GameStream stream, Player client)
        {
            byte[] uuid = Encoding.UTF8.GetBytes(client.UUID);
            VarInt uuidLength = uuid.Length;
            byte[] username = Encoding.UTF8.GetBytes(client.Username);
            VarInt usernameLength = username.Length;

            VarInt packetID = GetID();
            VarInt length = packetID.Length +
                            uuid.Length + uuidLength.Length +
                            username.Length + usernameLength.Length;

            stream.WriteVarInt(length);
            stream.WriteVarInt(packetID);
            stream.WriteVarInt(uuidLength);
            stream.WriteUInt8Array(uuid);
            stream.WriteVarInt(usernameLength);
            stream.WriteUInt8Array(username);
            stream.Flush();
        }

        public byte GetID()
        {
            return (byte) ClientLoginPacket.LoginSuccess;
        }

        public VarInt GetLength()
        {
            throw new NotImplementedException();
        }

        public SessionState GetState()
        {
            return SessionState.Login;
        }
    }
}
