using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.Entities.Metadata;
using GemsCraft.Worlds;

namespace GemsCraft.Entities
{
    public class FallingBlock: Entity
    {
        public EntityMetadata SpawnPosition = new EntityMetadata(
            6,
            EntityMetadataType.Position,
            new Location(0, 0, 0)
        );
    }
}
