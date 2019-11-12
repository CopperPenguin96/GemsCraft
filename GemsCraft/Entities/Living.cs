using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem.Types;
using GemsCraft.Entities.Metadata;

namespace GemsCraft.Entities
{
    public class Living: Entity
    {
        /// <summary>
        /// 0x01 = active
        /// 0x02 = Active hand
        /// 0x04 = riptide spin attack
        /// </summary>
        public EntityMetadata HandState = new EntityMetadata(
            6,
            EntityMetadataType.Byte,
            0
        );

        public EntityMetadata Health = new EntityMetadata(
            7,
            EntityMetadataType.Float,
            1.0
        );

        public EntityMetadata PotionEffectColor = new EntityMetadata(
            8,
            EntityMetadataType.VarInt,
            (VarInt) 0
        );

        public EntityMetadata PotionEffectAmbient = new EntityMetadata(
            9,
            EntityMetadataType.Boolean,
            false
        );

        public EntityMetadata Arrows = new EntityMetadata(
            10,
            EntityMetadataType.VarInt,
            (VarInt) 0
        );
    }
}
