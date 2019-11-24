namespace GemsCraft.Entities.Metadata.Flags
{
    public enum HorseFlags: byte
    {
        Unused = 0x01,
        IsTame = 0x02,
        IsSaddled = 0x04,
        HasBred = 0x08,
        IsEating = 0x10,
        IsRearingHindLegs = 0x20,
        IsMouthOpen = 0x40,
        UnusedOther = 0x80
    }
}
