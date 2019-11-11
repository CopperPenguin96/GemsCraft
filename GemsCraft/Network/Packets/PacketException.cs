using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemsCraft.Network.Packets
{
    public class PacketException : Exception
    {
        public PacketException(string message) : base(message)
        {

        }

        public PacketException(string message, Exception inner) : base(message, inner)
        {

        }

        public PacketException(Exception inner) : base("", inner)
        {

        }
    }
}
