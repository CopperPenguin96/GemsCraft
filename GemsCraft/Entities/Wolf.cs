using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem.Types;
using GemsCraft.Entities.Metadata;

namespace GemsCraft.Entities
{
    public class Wolf: TameableAnimal
    {
        public EntityMetadata DamageTaken = new EntityMetadata(
            15,
            EntityMetadataType.Float,
            1.0
        );

        public EntityMetadata IsBegging = new EntityMetadata(
            16,
            EntityMetadataType.Boolean,
            false
        );

        public EntityMetadata CollarColor = new EntityMetadata(
            17,
            EntityMetadataType.VarInt,
            (VarInt) 14 // Red
        );
    }
}
