using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.Worlds;

namespace GemsCraft.AppSystem.Types
{
    public class Position
    {
        public long X { get; set; }
        public long Y { get; set; }
        public long Z { get; set; }

        public long Get()
        {
            return ((X & 0x3FFFFFF) << 38) 
                   | ((Z & 0x3FFFFFF) << 12) 
                   | (Y & 0xFFF);
        }

        public Position(long value)
        {
            X = value >> 38;
            Y = value & 0xFFF;
            Z = (value << 26 >> 38);
        }

        public Position(long x, long y, long z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
