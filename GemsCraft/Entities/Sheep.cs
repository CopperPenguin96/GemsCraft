using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem.Types;
using GemsCraft.Entities.Metadata;

namespace GemsCraft.Entities
{
    public class Sheep: Animal
    {
        /// <summary>
        /// 0x0F for color,
        /// 0x10 for sheared
        /// </summary>
        public EntityMetadata ColorOrSheared = new EntityMetadata(
            13,
            EntityMetadataType.Byte,
            0
        );
    }
}
