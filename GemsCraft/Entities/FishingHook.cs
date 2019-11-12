using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.Entities.Metadata;

namespace GemsCraft.Entities
{
    public class FishingHook: Entity
    {
        public EntityMetadata EidOne = new EntityMetadata(
            6,
            EntityMetadataType.VarInt,
            0
        );
    }
}
