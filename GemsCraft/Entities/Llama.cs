using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem.Types;
using GemsCraft.Entities.Metadata;

namespace GemsCraft.Entities
{
    public class Llama: ChestedHorse
    {
        public EntityMetadata Stength = new EntityMetadata(
            16,
            EntityMetadataType.VarInt,
            (VarInt) 0
        );

        public EntityMetadata CarpetColor = new EntityMetadata(
            17,
            EntityMetadataType.VarInt,
            (VarInt) (-1)
        );

        /// <summary>
        /// 0 = creamy, 1 = white, 2 = brown, 3 = gray
        /// </summary>
        public EntityMetadata Variant = new EntityMetadata(
            18,
            EntityMetadataType.VarInt,
            (VarInt) 0
        );
    }
}
