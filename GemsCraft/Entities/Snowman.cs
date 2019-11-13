using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem.Types;
using GemsCraft.Entities.Metadata;

namespace GemsCraft.Entities
{
    public class Snowman
    {
        /// <summary>
        /// 0x10 = has pumpkin hat
        /// 0x00 = has no pumpkin hat
        /// </summary>
        public EntityMetadata PumpkinHat = new EntityMetadata(
            12,
            EntityMetadataType.Byte,
            0x10
        );
    }
}
