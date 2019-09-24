using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemsCraft.Network.Packets
{
    public delegate void Handle(byte id, params object[] args);

    public delegate void Receive(byte id, params object[] details);
    internal class PacketInfo
    {
        /// <summary>
        /// The total length of the packet, including ID
        /// </summary>
        public int Length;
        /// <summary>
        /// Identifier for the packet
        /// </summary>
        public int ID;
        /// <summary>
        /// Any infomoration being received/sent
        /// </summary>
        public byte[] Data;
        /// <summary>
        /// To be used when sending a packet
        /// </summary>
        public Handle Handler;
        /// <summary>
        /// To be used when received a packet
        /// </summary>
        public Receive Receiver;
    }
}
