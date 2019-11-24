namespace GemsCraft.Entities.Metadata.Flags
{
    public enum TameableFlags: byte
    {
        IsSitting = 0x01,
        /// <summary>
        /// Only used with wolves
        /// </summary>
        IsAngry = 0x02,
        IsTamed = 0x04
    }
}
