using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem.Types;
using GemsCraft.Entities.Metadata;

namespace GemsCraft.Entities
{
    public class TropicalFish: AbstractFish
    {
        public EntityMetadata Variant = new EntityMetadata(
            13,
            EntityMetadataType.VarInt,
            0
        );
    }
}
