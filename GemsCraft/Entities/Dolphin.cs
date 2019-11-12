using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem.Types;
using GemsCraft.Entities.Metadata;
using GemsCraft.Worlds;

namespace GemsCraft.Entities
{
    public class Dolphin: WaterMob
    {
        public EntityMetadata TreasurePosition = new EntityMetadata(
            12,
            EntityMetadataType.Position,
            new Location(0, 0, 0)
        );

        public EntityMetadata CantFindTreasure = new EntityMetadata(
            13,
            EntityMetadataType.Boolean,
            false
        );

        public EntityMetadata HasFish = new EntityMetadata(
            14,
            EntityMetadataType.Boolean,
            false
        );
    }
}
