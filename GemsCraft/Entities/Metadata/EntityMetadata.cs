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
    }
}
