namespace GemsCraft.Entities.Metadata.Flags
{
    public enum EntityEffect: byte
    {
        OnFire = 0x01,
        Crouched = 0x02,
        Unused = 0x04, // previously riding
        Sprinting = 0x08,
        Swimming = 0x10,
        Invisible = 0x20,
        Glowing = 0x40,
        FlyingWithElytra = 0x80
    }
}
