using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemsCraft.Network.Packets
{
    public class PacketItem<T>
    {
        public Type Type { get; }
        public T Value { get; }

        public PacketItem(T value)
        {
            Value = value;
            Type = Value.GetType();
        }
    }
}
