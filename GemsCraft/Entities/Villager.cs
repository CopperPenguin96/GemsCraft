using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem.Types;
using GemsCraft.Entities.Metadata;

namespace GemsCraft.Entities
{
    public class Villager : Ageable
    {

        public EntityMetadata Profession = new EntityMetadata(
            13,
            EntityMetadataType.VarInt,
            ProfessionType.Farmer
        );
    }

    public enum ProfessionType: int
    {
        Farmer = 0,
        Librarian = 1,
        Priest = 2,
        Blacksmith = 3,
        Butcher = 4,
        Nitwit = 5 // hahahahahahah what!
    }
}


