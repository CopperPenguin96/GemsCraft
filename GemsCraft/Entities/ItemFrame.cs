using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem.Types;
using GemsCraft.Entities.Metadata;

namespace GemsCraft.Entities
{
    public class ItemFrame: Hanging
    {
        public EntityMetadata Item = new EntityMetadata(
            6,
            EntityMetadataType.Slot,
            null
        );

        public EntityMetadata Rotation = new EntityMetadata(
            7,
            EntityMetadataType.VarInt,
            (VarInt) 0
        );
    }
}
