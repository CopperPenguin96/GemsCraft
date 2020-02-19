namespace GemsCraft.Players
{
    public enum ClickAction : byte
    {
        /// <summary>
        /// Deleting a block (left-click in Minecraft).
        /// </summary>
        Delete = 0,

        /// <summary>
        /// Building a block (right-click in Minecraft).
        /// </summary>
        Build = 1
    }
}
