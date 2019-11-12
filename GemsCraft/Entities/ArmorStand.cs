using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem.Types;
using GemsCraft.Entities.Metadata;

namespace GemsCraft.Entities
{
    public class ArmorStand: Living // This is why I don't trust the Minecraft devs
    {
        /// <summary>
        /// 0x01 = Is Small,
        /// 0x04 = Has Arms,
        /// 0x08 = No BasePlate
        /// 0x10 = Set Marker
        /// </summary>
        public EntityMetadata Setups = new EntityMetadata(
            11,
            EntityMetadataType.Byte,
            0
        );

        public EntityMetadata HeadRotation = new EntityMetadata(
            12,
            EntityMetadataType.Rotation,
            new Rotation(0.0, 0.0, 0.0)
        );

        public EntityMetadata BodyRotation = new EntityMetadata(
            13,
            EntityMetadataType.Rotation,
            new Rotation(0.0, 0.0, 0.0)
        );

        public EntityMetadata LeftArmRotation = new EntityMetadata(
            14,
            EntityMetadataType.Rotation,
            new Rotation(-10.0, 0.0, -10.0)
        );

        public EntityMetadata RightArmRotation = new EntityMetadata(
            15,
            EntityMetadataType.Rotation,
            new Rotation(-15.0, 0.0, 10.0)
        );

        public EntityMetadata LeftLegRotation = new EntityMetadata(
            16,
            EntityMetadataType.Rotation,
            new Rotation(-1.0, 0.0, -1.0)
        );

        public EntityMetadata RightLegRotation = new EntityMetadata(
            17,
            EntityMetadataType.Rotation,
            new Rotation(1.0, 0.0, 1.0)
        );
    }
}
