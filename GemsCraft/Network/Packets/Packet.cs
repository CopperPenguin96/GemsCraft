using GemsCraft.AppSystem;

namespace GemsCraft.Network.Packets
{
    internal class Packet
    {
        public static void ReceivePacket(SessionState state, VarInt id, GameStream stream)
        {
            byte packetID = (byte) id.Value;
            switch (state)
            {
                case SessionState.Handshaking:
                    break;
            }
        }
    }

  
}
