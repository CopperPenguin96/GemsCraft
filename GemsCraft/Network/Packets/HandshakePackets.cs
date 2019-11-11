using System.Net;
using GemsCraft.AppSystem.Logging;
using GemsCraft.AppSystem.Types;
using GemsCraft.Players;

namespace GemsCraft.Network.Packets
{
    internal class HandshakePackets
    {
        public static void ReceiveHandshake(Player client, GameStream stream)
        {
            VarInt protocolVersion = stream.ReadVarInt();
            string serverAddress = stream.ReadString();
            ushort port = stream.ReadShort();
            VarInt next = stream.ReadVarInt();

            if (!CheckHandshakeInfo(protocolVersion, serverAddress, port, next)) return;

            switch ((int) next.Value)
            {
                case 1: // Status
                    client.State = SessionState.Status;
                    StatusPackets.SendStatus(client, stream);
                    break;
                case 2: // Login
                    client.State = SessionState.Login;
                    break;
            }
        }

        private static bool CheckHandshakeInfo(VarInt pro, string add, ushort port, VarInt next)
        {
            if (pro < 404) return false; // First GemsCraft version
            if (add != "localhost")
            {
                if (!IPAddress.TryParse(add, out _))
                {
                    return false;
                }
            }

            if (port > 65535) return false;
            if (pro < Protocol.Current.Protocol)
            {
                Logger.Write("Outdated Client");
                return false;
            }

            if (pro > Protocol.Current.Protocol)
            {
                Logger.Write("Outdated Server");
                return false;
            }

            if (port > 65535)
            {
                Logger.Write("Invalid Port");
                return false;
            }

            if (next < 0 || next > 2)
            {
                Logger.Write("Invalid Session State");
                return false;
            }

            return true;
        }
    }
}
