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
        public void Receive(GameStream stream)
        {
            Player player = Player.CreateInstance(stream.ReadString());
        }

        public void Send(GameStream stream)
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
