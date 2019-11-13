using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem.Types;
using GemsCraft.Entities.Metadata;

namespace GemsCraft.Entities
{
    /// <summary>
    /// Index 13 is currently unused
    /// </summary>
    public class Zombie: Monster
    {
        public EntityMetadata IsBaby = new EntityMetadata(
            12,
            EntityMetadataType.Boolean,
            false
        );

        public EntityMetadata AreHandsUp = new EntityMetadata(
            14,
            EntityMetadataType.Boolean,
            false
        );

        public EntityMetadata IsBecomingDrowned = new EntityMetadata(
            15,
            EntityMetadataType.Boolean,
            false
        );
    }
}
