using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem.Types;
using GemsCraft.Entities.Metadata;

namespace GemsCraft.Entities
{
    public class Pig: Animal // Oink
    {
        public EntityMetadata HasSaddle = new EntityMetadata(
            13,
            EntityMetadataType.Boolean,
            false
        );

        public EntityMetadata TotalCarrotStickBoostTime = new EntityMetadata(
            14,
            EntityMetadataType.VarInt,
            (VarInt) 0
        );
    }
}
