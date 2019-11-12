using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.Entities.Metadata;

namespace GemsCraft.Entities
{
    public class Arrow: Entity
    {
        /// <summary>
        /// Is 0x01 if is critical, 0x02 if is noclip
        /// </summary>
        public EntityMetadata Hit = new EntityMetadata(
            6,
            EntityMetadataType.Byte,
            0x01
        );

        public EntityMetadata ShooterUUID = new EntityMetadata(
            7,
            EntityMetadataType.OptUUID,
            null
        );
    }
}
