using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.Entities.Metadata;

namespace GemsCraft.Entities
{
    public class EnderCrystal: Entity
    {
        public EntityMetadata BeamTarget = new EntityMetadata(
            6,
            EntityMetadataType.OptPosition,
            null
        );

        public EntityMetadata ShowBottom = new EntityMetadata(
            7,
            EntityMetadataType.Boolean,
            true
        );
    }
}
