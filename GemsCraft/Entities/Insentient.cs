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
        /// <summary>
        /// 0x01 if No AI, 0x02 if left handed
        /// </summary>
        public EntityMetadata HandAndAI = new EntityMetadata(
            11,
            EntityMetadataType.Byte,
            0
        );
    }
}
