namespace GemsCraft.Entities.Metadata.Flags
{
    public enum HandState: byte
    {
        IsHandActive = 0x01,
        /// <summary>
        /// Set to false if main hand, set to true if offhand
        /// </summary>
        ActiveHand = 0x02,
        RiptideSpinAttack = 0x04
    }
}
