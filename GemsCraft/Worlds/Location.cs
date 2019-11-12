using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem.Types;

namespace GemsCraft.Worlds
{
    public class Location
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public byte Pitch { get; set; }
        public byte Yaw { get; set; }
        public byte HeadPitch { get; set; }

        public Location(double x, double y, double z, byte pit = 0, byte yaw = 0, byte head = 0)
        {
            X = x;
            Y = y;
            Z = z;
            Pitch = pit;
            Yaw = yaw;
            HeadPitch = head;
        }
        
        /// <summary>
        /// The length of a location object when its sent to the client
        /// only ever 26 because the byte length is 1 and the double
        /// length is 8
        /// </summary>
        public static readonly VarInt Length = 26;
    }
}
