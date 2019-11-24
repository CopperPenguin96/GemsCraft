using GemsCraft.Entities.Metadata;

namespace GemsCraft.Entities
{
    public class AbstractHorse: Animal
    {
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
