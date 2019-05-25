using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem;
using GemsCraft.Players;

namespace GemsCraft.Network.Packets.LoginPackets
{
    internal class LoginStartPacket : IPacket
    {
        public void Receive(GameStream stream, Player client)
        {
            Player player = Player.CreateInstance(stream.ReadString());
            PlayerDB.LoadPlayerDB();
            Protocol.EncryptionRequestPacket.Send(stream, client); // Next step in login sequence, requesting encryption
            // TODO if no encryption jump to login
        }

        public void Send(GameStream stream, Player client)
        {
            throw new NotImplementedException();
        }

        public byte GetID()
        {
            return 0x00;
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
