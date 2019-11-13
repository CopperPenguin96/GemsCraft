using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.Entities.Metadata;

namespace GemsCraft.Entities
{
    public class Ghast: Flying
    {
        public EntityMetadata IsAttacking = new EntityMetadata(
            12,
            EntityMetadataType.Boolean,
            false
        );
    }
}
