using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.Entities.Metadata;

namespace GemsCraft.Entities
{
    public class Enderman: Monster
    {
        public EntityMetadata CarriedBlock = new EntityMetadata(
            12,
            EntityMetadataType.OptBlockID,
            null
        );

        public EntityMetadata IsScreaming = new EntityMetadata(
            13,
            EntityMetadataType.Boolean,
            false
        );
    }
}
