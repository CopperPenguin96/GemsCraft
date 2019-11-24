using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fNbt;

namespace GemsCraft.AppSystem.Types
{
    /// <summary>
    /// The slot data structure is how Minecraft represents an item
    /// and its associated data in the Minecraft protocol
    /// </summary>
    public class Slot
    {
        public bool Present { get; set; }
        public VarInt ItemID { get; set; }
        public byte ItemCount { get; set; }
        public NbtCompound OptionalNbt { get; set; }
    }
}
