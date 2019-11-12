using GemsCraft.AppSystem.Types;
using GemsCraft.Entities.Metadata;

namespace GemsCraft.Entities
{
    public class Boat: Entity
    {
        public EntityMetadata TimeSinceLastHit = new EntityMetadata(
            6,
            EntityMetadataType.VarInt,
            (VarInt) 0
        );

        public EntityMetadata ForwardDirection = new EntityMetadata(
            7,
            EntityMetadataType.VarInt,
            (VarInt) 1
        );

        public EntityMetadata DamageTaken = new EntityMetadata(
            8,
            EntityMetadataType.Float,
            0.0
        );

        /// <summary>
        /// 0 = oak; 1 = spruce; 2 = birch, 3 = jungle;
        /// 4 = acacia; 5 = dark oak
        /// </summary>
        public EntityMetadata Type = new EntityMetadata(
            9,
            EntityMetadataType.VarInt,
            (VarInt) 0
        );

        public EntityMetadata LeftPaddleTurning = new EntityMetadata(
            10,
            EntityMetadataType.Boolean,
            false
        );

        public EntityMetadata RightPaddleTurning = new EntityMetadata(
            11,
            EntityMetadataType.Boolean,
            false
        );

        public EntityMetadata SplashTimer = new EntityMetadata(
            12,
            EntityMetadataType.VarInt,
            (VarInt) 0
        );
    }
}
