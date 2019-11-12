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
    public class Turtle: Animal
    {
        public EntityMetadata HomePos = new EntityMetadata(
            13,
            EntityMetadataType.Position,
            new Location(0, 0, 0)
        );

        public EntityMetadata HasEgg = new EntityMetadata(
            14,
            EntityMetadataType.Boolean,
            false
        );

        public EntityMetadata LayingEgg = new EntityMetadata(
            15,
            EntityMetadataType.Boolean,
            false
        );

        public EntityMetadata TravelPos = new EntityMetadata(
            16,
            EntityMetadataType.Position,
            new Location(0, 0, 0)
        );

        public EntityMetadata GoingHome = new EntityMetadata(
            17,
            EntityMetadataType.Boolean,
            false
        );

        public EntityMetadata Traveling = new EntityMetadata(
            18,
            EntityMetadataType.Boolean,
            false
        );
    }
}
