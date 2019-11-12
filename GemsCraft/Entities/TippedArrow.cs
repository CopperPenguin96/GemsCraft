using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.Entities.Metadata;

namespace GemsCraft.Entities
{
    public class TippedArrow: Arrow
    {
        /// <summary>
        /// -1 for no color
        /// </summary>
        public EntityMetadata Color = new EntityMetadata(
            8,
            EntityMetadataType.VarInt,
            -1
        );
    }
}
