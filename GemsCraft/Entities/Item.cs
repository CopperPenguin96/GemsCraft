using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.Entities.Metadata;

namespace GemsCraft.Entities
{
    public class Item: Entity
    {
        public EntityMetadata ItemSlot = new EntityMetadata(
            6,
            EntityMetadataType.Slot,
            null
        );
    }
}
