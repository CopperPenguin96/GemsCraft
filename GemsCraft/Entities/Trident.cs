using GemsCraft.AppSystem.Types;
using GemsCraft.Entities.Metadata;

namespace GemsCraft.Entities
{
    public class Trident: Arrow
    {
        public EntityMetadata LoyaltyLevel = new EntityMetadata(
            8,
            EntityMetadataType.VarInt,
            (VarInt) 0
        );
    }
}
