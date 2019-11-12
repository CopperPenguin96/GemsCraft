using GemsCraft.Entities.Metadata;

namespace GemsCraft.Entities
{
    public class AbstractFish: WaterMob
    {
        public EntityMetadata FromBucket = new EntityMetadata(
            12,
            EntityMetadataType.Boolean,
            false
        );
    }
}
