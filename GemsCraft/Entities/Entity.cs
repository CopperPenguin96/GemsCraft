using GemsCraft.AppSystem.Types;
using GemsCraft.Chat;
using GemsCraft.Entities.Metadata;
using GemsCraft.Entities.Metadata.Flags;

namespace GemsCraft.Entities
{
    /// <summary>
    /// The base class for entities
    /// </summary>
    public class Entity
    {
        public EntityMetadata Effect = new EntityMetadata(
            0, 
            EntityMetadataType.Byte, 
            0);

        public EntityMetadata Air = new EntityMetadata(
            1,
            EntityMetadataType.VarInt,
            (VarInt) 300);

        public EntityMetadata CustomName = new EntityMetadata(
            2,
            EntityMetadataType.OptChat,
            new OptChat(false, null));

        public EntityMetadata CustomNameVisible = new EntityMetadata(
            3,
            EntityMetadataType.Boolean,
            false);

        public EntityMetadata IsSilent = new EntityMetadata(
            4,
            EntityMetadataType.Boolean,
            false);

        public EntityMetadata NoGravity = new EntityMetadata(
            5,
            EntityMetadataType.Boolean,
            false);
    }
}
