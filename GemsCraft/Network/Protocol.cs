using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem;
using GemsCraft.Configuration;
using GemsCraft.Players;

namespace GemsCraft.Network
{
    internal class Protocol
    {
        public const int Version = 404;

        public static string Handshake(SessionState state, VarInt protVer, string ip, ushort port, VarInt nextState)
        {
            return null;
        }
    }
}
