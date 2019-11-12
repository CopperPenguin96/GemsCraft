using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemsCraft.AppSystem.Types
{
    public class Rotation
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Rotation(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
