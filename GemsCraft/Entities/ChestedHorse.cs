using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem.Types;
using GemsCraft.Entities.Metadata;

namespace GemsCraft.Entities
{
    public class ChestedHorse: AbstractHorse
    {
        public EntityMetadata HasChest = new EntityMetadata(
            15,
            EntityMetadataType.Boolean,
            false
        );
    }
}
