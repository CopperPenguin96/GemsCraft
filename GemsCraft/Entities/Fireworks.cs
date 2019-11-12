using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem.Types;
using GemsCraft.Entities.Metadata;

namespace GemsCraft.Entities
{
    public class Fireworks: Entity
    {
        public EntityMetadata Info = new EntityMetadata(
            6,
            EntityMetadataType.Slot,
            null
        );

        public EntityMetadata EntityID = new EntityMetadata(
            7,
            EntityMetadataType.VarInt,
            (VarInt) 0
        );
    }
}
