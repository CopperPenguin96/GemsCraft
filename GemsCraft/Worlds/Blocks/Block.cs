using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemsCraft.Worlds.Blocks
{
    public class Block
    {
        /// <summary>
        /// The identifier of the Block i.e. Granite is 1:1
        /// </summary>
        public BlockID ID { get; set; }

        /// <summary>
        /// The name of the block i.e. Granite
        /// </summary>
        public string Name;

        public FullID FullID { get; set; }

        private int _stack;
        public int StackSize
        {
            get => _stack;
            set
            {
                if (value > 64) throw new ArgumentOutOfRangeException();

                _stack = value;
            }
        }

        public double Hardness { get; set; }
        public BlockID MinStateID { get; set; }
        public BlockID MaxStateID { get; set; }
        public BlockState[] States { get; set; }
        public BlockID[] Drops { get; set; }
        public bool Diggable { get; set; }
        public bool Transparent { get; set; }
        public int FilterLight { get; set; }
        public int EmitLight { get; set; }
        public string BoundingBox { get; set; }
        public string Material { get; set; }
        public BlockID[] HarvestTools { get; set; }

        public Block(int id1, int id2)
        {
            ID = new BlockID(id1, id2);
        }

        public Block(BlockID id, string name, FullID fId)
        {
            ID = id ?? throw new ArgumentNullException(nameof(id));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            FullID = fId ?? throw new ArgumentNullException(nameof(fId));
        }
    }

    public class FullID
    {
        public string Namespace;
        public string Name;

        public FullID(string s, string y)
        {
            Namespace = s ?? throw new ArgumentNullException(nameof(s));
            Name = y ?? throw new ArgumentNullException(nameof(y));
        }

        public override string ToString()
        {
            return $"{Namespace}:{Name}";
        }
    }

    public class BlockID
    {
        public int Item;
        public int SubItem;

        public BlockID(int i1, int i2)
        {
            Item = i1;
            SubItem = i2;
        }

        public override string ToString()
        {
            return $"{Item}:{SubItem}";
        }
    }
}
