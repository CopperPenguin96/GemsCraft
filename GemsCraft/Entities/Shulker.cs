using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem.Types;
using GemsCraft.Entities.Metadata;

namespace GemsCraft.Entities
{
    public class Shulker: Golem
    {
        public EntityMetadata AttachFace = new EntityMetadata(
            12,
            EntityMetadataType.Direction,
            0
        );

        public EntityMetadata AttachmentPosition = new EntityMetadata(
            13,
            EntityMetadataType.OptPosition,
            null
        );

        public EntityMetadata ShieldHeight = new EntityMetadata(
            14,
            EntityMetadataType.Byte,
            0
        );

        public EntityMetadata DyeColor = new EntityMetadata(
            15,
            EntityMetadataType.Byte,
            10 // Purple
        );
    }
}
