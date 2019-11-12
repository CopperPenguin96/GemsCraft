using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem.Types;
using GemsCraft.Entities.Metadata;

namespace GemsCraft.Entities
{
    public class PolarBear: Animal
    {
        public EntityMetadata StandingUp = new EntityMetadata(
            13,
            EntityMetadataType.Boolean,
            false
        );
    }
}
