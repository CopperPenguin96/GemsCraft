using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem.Types;
using GemsCraft.Entities.Metadata;

namespace GemsCraft.Entities
{
    public class Slime: Insentient
    {
        public EntityMetadata Size = new EntityMetadata(
            12,
            EntityMetadataType.VarInt,
            (VarInt) 1
        );
    }
}
