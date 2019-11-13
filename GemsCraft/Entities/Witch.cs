using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem.Types;
using GemsCraft.Entities.Metadata;

namespace GemsCraft.Entities
{
    public class Witch: Monster
    {
        public EntityMetadata IsDrinkingPotion = new EntityMetadata(
            12,
            EntityMetadataType.Boolean,
            false
        );
    }
}
