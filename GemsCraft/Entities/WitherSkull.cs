using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.Entities.Metadata;

namespace GemsCraft.Entities
{
    public class WitherSkull: AbstractFireball
    {
        public EntityMetadata Invulnerable = new EntityMetadata(
            6,
            EntityMetadataType.Boolean,
            false
        );
    }
}
