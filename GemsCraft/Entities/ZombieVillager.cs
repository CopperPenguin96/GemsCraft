using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem.Types;
using GemsCraft.Entities.Metadata;

namespace GemsCraft.Entities
{
    public class ZombieVillager: Zombie
    {
        public EntityMetadata IsConverting = new EntityMetadata(
            16,
            EntityMetadataType.Boolean,
            false
        );

        public EntityMetadata Profession = new EntityMetadata(
            17,
            EntityMetadataType.VarInt,
            ProfessionType.Farmer
        );
    }
}
