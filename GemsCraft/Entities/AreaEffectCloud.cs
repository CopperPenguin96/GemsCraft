using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.Entities.Metadata;

namespace GemsCraft.Entities
{
    public class AreaEffectCloud: Entity
    {
        public EntityMetadata Radius = new EntityMetadata(
            6,
            EntityMetadataType.Float,
            0.5
        );

        public EntityMetadata Color = new EntityMetadata(
            7,
            EntityMetadataType.VarInt,
            0
        );

        public EntityMetadata IgnoreRadiusSinglePoint = new EntityMetadata(
            8,
            EntityMetadataType.Boolean,
            false
        );

        public EntityMetadata Particle = new EntityMetadata(
            9,
            EntityMetadataType.Particle,
            null
        );
    }
}
