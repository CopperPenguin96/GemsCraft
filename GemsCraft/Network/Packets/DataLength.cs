using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem;

namespace GemsCraft.Network.Packets
{
    /// <summary>
    /// Packet data sizes for fixed types.
    /// This does not include things that are based
    /// on variables i.e. string
    /// </summary>
    internal class DataLength
    {
        public const int Boolean = 1;
        public const int Byte = 1;
        public const int UByte = 1;
        public const int Short = 2;
        public const int UShort = 2;
        public const int Int = 4;
        public const int Long = 8;
        public const int Float = 4;
        public const int Double = 8;
        public const int Position = 8;
        public const int Angle = 1;
        public const int UUID = 16;
    }
}
