using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemsCraft.Network.Packets.LoginPackets
{
    internal enum ServerLoginPacket
    {
        LoginStart = 0x00,
        EncryptionResponse = 0x01,
        LoginPluginResponse = 0x02
    }
}
