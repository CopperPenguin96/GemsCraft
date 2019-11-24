using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem.Types;
using GemsCraft.Entities.Metadata;

namespace GemsCraft.Entities
{
    public class Insentient: Living
    {
        public EntityMetadata HandAndAI = new EntityMetadata(
            11,
            EntityMetadataType.Byte,
            0
        );
    }
}
