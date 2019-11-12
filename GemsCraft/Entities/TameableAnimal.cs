using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem.Types;
using GemsCraft.Entities.Metadata;

namespace GemsCraft.Entities
{
    public class TameableAnimal: Animal
    {
        /// <summary>
        /// 0x01 for sitting
        /// 0x02 for angry (only for wolves)
        /// 0x04 for tamed
        /// </summary>
        public EntityMetadata Action = new EntityMetadata(
            13,
            EntityMetadataType.Byte,
            0
        );

        public EntityMetadata Owner = new EntityMetadata(
            14,
            EntityMetadataType.OptUUID,
            null
        );
    }
}
