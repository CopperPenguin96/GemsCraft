using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem.Types;
using GemsCraft.Entities.Metadata;

namespace GemsCraft.Entities
{
    public class PufferFish: AbstractFish
    {
        /// <summary>
        /// varies from 0 to 2
        /// </summary>
        public EntityMetadata PuffState = new EntityMetadata(
            13,
            EntityMetadataType.VarInt,
            (VarInt) 0
        );
    }
}
