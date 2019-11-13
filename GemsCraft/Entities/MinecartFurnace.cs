using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.Entities.Metadata;

namespace GemsCraft.Entities
{
    public class MinecartFurnace: Minecart
    {
        public EntityMetadata HasFuel = new EntityMetadata(
            12,
            EntityMetadataType.Boolean,
            false
        );
    }
}
