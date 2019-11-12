using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem.Types;
using GemsCraft.Entities.Metadata;

namespace GemsCraft.Entities
{
    public class AbstractHorse: Animal
    {
        /// <summary>
        /// 0x01 = unused
        /// 0x02 = Is Tame
        /// 0x04 = Is Saddled
        /// 0x08 = Has Bred
        /// 0x10 = Is Eating
        /// 0x20 = Is Rearing (On Hind Legs)
        /// 0x40 = Mouth Open
        /// 0x80 = Unused
        /// </summary>
        public EntityMetadata Info = new EntityMetadata(
            13,
            EntityMetadataType.Byte,
            0
        );

        public EntityMetadata Owner = new EntityMetadata(
            13,
            EntityMetadataType.OptUUID,
            null
        );
    }
}
