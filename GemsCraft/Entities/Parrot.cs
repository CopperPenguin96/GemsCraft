using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem.Types;
using GemsCraft.Entities.Metadata;

namespace GemsCraft.Entities
{
    public class Parrot: TameableAnimal
    {
        /// <summary>
        /// 0 = red/blue,
        /// 1 = blue,
        /// 2 = green,
        /// 3 = yellow/blue,
        /// 4 = grey
        /// </summary>
        public EntityMetadata Type = new EntityMetadata(
            15,
            EntityMetadataType.VarInt,
            (VarInt) 0
        );
    }
}
