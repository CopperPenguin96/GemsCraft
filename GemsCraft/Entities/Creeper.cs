using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem.Types;
using GemsCraft.Entities.Metadata;

namespace GemsCraft.Entities
{
    public class Creeper: Monster
    {
        /// <summary>
        /// -1 = idle,
        /// 1 = fuse
        /// </summary>
        public EntityMetadata State = new EntityMetadata(
            12,
            EntityMetadataType.VarInt,
            (VarInt) (-1)
        );

        public EntityMetadata IsCharged = new EntityMetadata(
            13,
            EntityMetadataType.Boolean,
            false
        );

        public EntityMetadata IsIgnited = new EntityMetadata(
            14,
            EntityMetadataType.Boolean,
            false
        );
    }
}
