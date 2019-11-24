using GemsCraft.Entities.Metadata;
using GemsCraft.Entities.Metadata.Flags;

namespace GemsCraft.Entities
{
    public class Arrow: Entity
    {
        public EntityMetadata Hit = new EntityMetadata(
            6,
            EntityMetadataType.Byte,
            0
        );

        public EntityMetadata ShooterUUID = new EntityMetadata(
            7,
            EntityMetadataType.OptUUID,
            null
        );
    }
}
