using GemsCraft.Entities.Metadata;

namespace GemsCraft.Entities
{
    public class AbstractIllager: Monster
    {
        public EntityMetadata HasTarget = new EntityMetadata(
            12,
            EntityMetadataType.Byte,
            0
        );
    }
}
