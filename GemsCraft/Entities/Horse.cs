using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem.Types;
using GemsCraft.Entities.Metadata;

namespace GemsCraft.Entities
{
    public class Horse: AbstractHorse
    {
        public EntityMetadata ColorAndStyle = new EntityMetadata(
            15,
            EntityMetadataType.VarInt,
            (VarInt) 0
        );

        /// <summary>
        /// 0 = none, 1 = iron, 2 = gold, 3 = diamond
        /// </summary>
        public EntityMetadata Armor = new EntityMetadata(
            16,
            EntityMetadataType.VarInt,
            0
        );

        public EntityMetadata ArmorItem = new EntityMetadata(
            17,
            EntityMetadataType.Slot,
            null
        );
    }
}
