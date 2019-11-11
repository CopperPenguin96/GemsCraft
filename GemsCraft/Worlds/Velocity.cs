using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem.Types;

namespace GemsCraft.Worlds
{
    public class Velocity
    {
        public short X { get; set; }
        public short Y { get; set; }
        public short Z { get; set; }

        public Velocity(short x, short y, short z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// The length of a velocity object when its sent to the client
        /// only ever 6 because the short length is 2
        /// </summary>
        public static readonly VarInt Length = 6;
    }
}
