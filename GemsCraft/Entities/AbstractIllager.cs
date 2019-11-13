using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem.Types;
using GemsCraft.Entities.Metadata;

namespace GemsCraft.Entities
{
    public class AbstractIllager: Monster
    {
        /// <summary>
        /// For use while in aggressive state,
        /// if true 0x01
        /// </summary>
        public EntityMetadata HasTarget = new EntityMetadata(
            12,
            EntityMetadataType.Byte,
            0
        );
    }
}
