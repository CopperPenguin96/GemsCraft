using System;
using GemsCraft.Utils;

namespace GemsCraft.Entities.Metadata
{
    public class EntityMetadata
    {
        public byte Index { get; set; }
        public EntityMetadataType Type { get; set; }
        public object Value { get; set; }

        public EntityMetadata(byte index, EntityMetadataType type, object value)
        {
            Index = index;
            Type = type;
            Value = value;
        }

        public void SetMaskValue(Enum value, bool on)
        {
            if (Type != EntityMetadataType.Byte)
                throw new ArgumentException("Type must be of byte");
            Value = on.ToByte() << Convert.ToByte(value);
        }
    }
}
