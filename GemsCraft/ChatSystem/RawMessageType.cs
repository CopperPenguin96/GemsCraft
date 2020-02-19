namespace GemsCraft.ChatSystem
{
    /// <summary>
    /// Type of message sent by the player. Determined by CommandManager.GetMessageType()
    /// </summary>
    public enum RawMessageType
    {
        /// <summary>
        /// Unparseable chat syntax (rare).
        /// </summary>
        Invalid,

        /// <summary>
        /// Normal (white) chat.
        /// </summary>
        Chat,

        /// <summary>
        /// Command.
        /// </summary>
        Command,

        /// <summary>
        /// Confirmation (/ok) for a previous command.
        /// </summary>
        Confirmation,

        /// <summary>
        /// Partial message (ends with " /").
        /// </summary>
        PartialMessage,

        /// <summary>
        /// Private message.
        /// </summary>
        PrivateChat,

        /// <summary>
        /// Rank chat.
        /// </summary>
        RankChat,

        /// <summary>
        /// Repeat of the last command ("/").
        /// </summary>
        RepeatCommand,

        /// <summary>
        /// Chat private to the world you are in.
        /// </summary>
        WorldChat,

        /// <summary>
        /// LongerMessages partial message.
        /// </summary>
        LongerMessage,
    }
}
