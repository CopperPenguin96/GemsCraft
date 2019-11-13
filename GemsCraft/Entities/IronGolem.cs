using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem.Types;
using GemsCraft.Entities.Metadata;

namespace GemsCraft.Entities
{
    public class IronGolem: Golem
    {
        /// <summary>
        /// 0x01 = player created
        /// </summary>
        public EntityMetadata PlayerCreated = new EntityMetadata(
            12,
            EntityMetadataType.Boolean,
            0
        );
    }
}
