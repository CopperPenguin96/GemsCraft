using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemsCraft.Network.Packets.LoginPackets
{
    internal enum ClientLoginPacket
    {
        Disconnect = 0x00,
        EncryptionRequest = 0x01,
        LoginSuccess = 0x02,
        SetCompression = 0x03,
        LoginPluginRequest = 0x04
    }
}
