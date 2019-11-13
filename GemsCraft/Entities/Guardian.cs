using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem.Types;
using GemsCraft.Entities.Metadata;

namespace GemsCraft.Entities
{
    public class Guardian: Monster
    {
        public EntityMetadata IsRetractingSpikes = new EntityMetadata(
            12,
            EntityMetadataType.Boolean,
            false
        );

        public EntityMetadata TargetEID = new EntityMetadata(
            13,
            EntityMetadataType.VarInt,
            (VarInt) 0
        );
    }
}
