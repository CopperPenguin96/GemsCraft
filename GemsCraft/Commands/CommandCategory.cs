namespace GemsCraft.Commands
{
    public enum CommandCategory
    {
        /// <summary>
        /// Default command category. Do not use it.
        /// </summary>
        None = 0,

        /// <summary>
        /// Building-related commands: drawing, binding, copy/paste.
        /// </summary>
        Building = 1,

        /// <summary>
        /// Chat-related commands: messaging, ignoring, muting, etc.
        /// </summary>
        Chat = 2,

        /// <summary>
        /// Information commands: server, world, zone, rank, and player infos.
        /// </summary>
        Info = 4,

        /// <summary>
        /// Moderation commands: kick, ban, rank, tp/bring, etc.
        /// </summary>
        Moderation = 8,

        /// <summary>
        /// Server maintenance commands: reloading configs, editing PlayerDB, importing data, etc.
        /// </summary>
        Maintenance = 16,

        /// <summary>
        /// World-related commands: creating, editing, testing, etc.
        /// </summary>
        World = 32,

        /// <summary>
        /// Zone-related commands: creating, editing, testing, etc.
        /// </summary>
        Zone = 64,
        
        /// <summary>
        /// Commands that are only used for diagnostics and debugging.
        /// </summary>
        Debug = 128,

        /// <summary>
        /// Commands that are just for fun.
        /// </summary>
        Fun = 256,

        /// <summary>
        /// COmmands that used advanced mathematics.
        /// </summary>
        Math = 512
    }
}
