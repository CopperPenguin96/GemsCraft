using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem.Types;

namespace GemsCraft.Network.Packets
{
    internal enum Packet : byte
    {
        // Handshaking Packets
        Handshake = 0x00, // Serverbound

        // Status Packets
        Response = 0x00, // Clientbound
        Ping = 0x01, // Serverbound
        Pong = 0x01, // Clientbound

        // Login Packets
        LoginStart = 0x00, // Serverbound
        LoginDisconnect = 0x00, // Clientbound
        EncryptionRequest = 0x01, // Clientbound
        EncryptionResponse = 0x01, // Serverbound
        LoginSuccess = 0x02 // Clientbound
    }
}
