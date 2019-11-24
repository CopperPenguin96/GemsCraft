namespace GemsCraft.Entities.Metadata.Flags
{
    public enum SkinParts: byte
    {
        CapeEnabled = 0x01,
        JacketEnabled = 0x02,
        LeftSleeveEnabled = 0x04,
        RightSleeveEnabled = 0x08,
        LeftPantsLegEnabled = 0x10,
        RightPantsLegEnabled = 0x20,
        HatEnabled = 0x40,
        Unused = 0x80
    }
}
