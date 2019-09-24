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
        Handshake = 0x00,
        Response = 0x00,
        Ping = 0x01,
        Pong = 0x01,

        // Login Packets
        LoginStart = 0x00,
        EncryptionRequest = 0x01,
        EncryptionResponse = 0x01,
        LoginDisconnect = 0x00
    }
}
