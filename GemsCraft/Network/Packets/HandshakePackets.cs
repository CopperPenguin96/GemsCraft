using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem;
using GemsCraft.AppSystem.Types;
using GemsCraft.Configuration;
using GemsCraft.Properties;
using GemsCraft.Utils;

namespace GemsCraft.Network.Packets
{
    internal class HandshakePackets
    {
        #region Info & Status

        public static void ReceiveHandshake(ref GameStream stream)
        {
            VarInt protocolVersion = stream.ReadVarInt();
            string serverAddress = stream.ReadString();
            ushort port = stream.ReadUInt16();
            VarInt next = stream.ReadVarInt();
            
            if (!CheckHandshakeInfo(protocolVersion, serverAddress, port, next)) return;

            switch ((int) next.Value)
            {
                case 1: // Status
                    SendStatus(ref stream);
                    break;
                case 2: // Login
                    stream.State = SessionState.Login;
                    break;
            }

        }

        private static bool CheckHandshakeInfo(VarInt pro, string add, ushort port, VarInt next)
        {
            if (pro < 498) return false; // First GemsCraft version
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

        public static void SendStatus(ref GameStream stream)
        {
            int id = (byte) Packet.Response;
            ResponseData data = new ResponseData
            {
                Version = Protocol.Current,
                Players = new PlayerStatusList(),
                Icon = "data:image/png;base64," + new ServerIcon(Resources.server_icon)
            };

            data.Save(); // Save for debug purposes
            string details = data.GetJson();
            byte[] detsBytes = Encoding.UTF8.GetBytes(details);
            VarInt detsLength = detsBytes.Length;
            VarInt idLength = ((VarInt) id).Length + detsLength.Length;

            stream.WriteVarInt(idLength + detsLength);
            stream.WriteVarInt(id);
            stream.WriteVarInt(detsLength);
            stream.WriteUInt8Array(detsBytes);
        }

        #endregion

        public static void ReceivePing(ref GameStream stream)
        {
            long payload = stream.ReadInt64();
            stream.Payload = payload;
            SendPing(ref stream);
        }

        public static void SendPing(ref GameStream stream)
        {
            VarInt id = (byte) Packet.Pong;
            VarInt length = id.Length + 8; // 8 for Long length
            stream.WriteVarInt(length);
            stream.WriteVarInt(id);
            stream.WriteInt64(stream.Payload);
        }
    }
}
