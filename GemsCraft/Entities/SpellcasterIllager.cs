using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem.Types;
using GemsCraft.Entities.Metadata;

namespace GemsCraft.Entities
{
    public class SpellcasterIllager: AbstractIllager
    {
        public EntityMetadata Spell = new EntityMetadata(
            13,
            EntityMetadataType.Byte,
            (Spell) 0
        );
    }

    public enum Spell: byte
    {
        None = 0,
        SummonVex = 1,
        Attack = 2,
        Wololo = 3,
        Disappear = 4,
        Blindness = 5
    }

}
