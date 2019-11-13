using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem.Types;
using GemsCraft.Entities.Metadata;

namespace GemsCraft.Entities
{
    public class Minecart: Entity
    {
        public EntityMetadata ShakingPower = new EntityMetadata(
            6,
            EntityMetadataType.VarInt,
            0
        );

        public EntityMetadata ShakingDirection = new EntityMetadata(
            7,
            EntityMetadataType.VarInt,
            (VarInt) 1
        );

        public EntityMetadata ShakingMultiplayer = new EntityMetadata(
            8,
            EntityMetadataType.Float,
            0.0
        );

        public EntityMetadata CustomBlockIDAndDamage = new EntityMetadata(
            9,
            EntityMetadataType.VarInt,
            (VarInt) 0
        );

        /// <summary>
        /// in 16ths of a block
        /// </summary>
        public EntityMetadata CustomBlockYPosition = new EntityMetadata(
            10,
            EntityMetadataType.VarInt,
            (VarInt) 6
        );

        public EntityMetadata ShowCustomBlock = new EntityMetadata(
            11,
            EntityMetadataType.Boolean,
            false
        );
    }
}
