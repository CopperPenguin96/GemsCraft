using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem.Types;
using GemsCraft.Entities.Metadata;

namespace GemsCraft.Entities
{
    public class Wither: Monster
    {
        /// <summary>
        /// Entity ID, or 0 if no target
        /// </summary>
        public EntityMetadata CenterHeadTarget = new EntityMetadata(
            12,
            EntityMetadataType.VarInt,
            (VarInt) 0
        );

        /// <summary>
        /// Entity ID, or 0 if no target
        /// </summary>
        public EntityMetadata LeftHeadTarget = new EntityMetadata(
            13,
            EntityMetadataType.VarInt,
            (VarInt) 0
        );

        /// <summary>
        /// EntityID, or 0 if no target
        /// </summary>
        public EntityMetadata RightHeadTarget = new EntityMetadata(
            14,
            EntityMetadataType.VarInt,
            0
        );

        public EntityMetadata InvulnerableTime = new EntityMetadata(
            15,
            EntityMetadataType.VarInt,
            (VarInt) 0
        );
    }
}
