using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem.Types;
using GemsCraft.Entities.Metadata;

namespace GemsCraft.Entities
{
    public class TNTPrimed: Entity
    {
        public EntityMetadata FuseTime = new EntityMetadata(
            6,
            EntityMetadataType.VarInt,
            (VarInt) 80
        );
    }
}
