namespace GemsCraft.Worlds
{
    public enum WorldChangeReason
    {
        /// <summary>
        /// First world that the player joins upon entering the server.
        /// </summary>
        FirstWorld,

        /// <summary>
        /// Rejoining the same world
        /// </summary>
        Rejoin,

        /// <summary>
        /// Teleporting to a player on another map
        /// </summary>
        Teleport,

        /// <summary>
        /// Bring brought by a player on another map.
        /// </summary>
        Bring,

        /// <summary>
        /// Following the /spectate target to another world.
        /// </summary>
        SpectateTargetJoined,

        /// <summary>
        /// Previous world was removed with /wunload
        /// </summary>
        WorldRemoved,

        /// <summary>
        /// Previous world's permissions changed
        /// </summary>
        PermissionChanged,

        /// <summary>
        /// Player entered a portal
        /// </summary>
        Portal
    }
}
