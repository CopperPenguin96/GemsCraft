using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem.Types;
using GemsCraft.Entities.Metadata;

namespace GemsCraft.Entities
{
    public class Rabbit: Animal
    {
        public EntityMetadata Type = new EntityMetadata(
            13,
            EntityMetadataType.VarInt,
            (VarInt) 0
        );
    }
}
