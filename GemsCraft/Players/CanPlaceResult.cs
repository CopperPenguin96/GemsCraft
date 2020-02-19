namespace GemsCraft.Players
{
    /// <summary>
    /// A list of possible results of Player.CanPlace() permission test.
    /// </summary>
    public enum CanPlaceResult
    {

        /// <summary>
        /// Block may be placed/changed.
        /// </summary>
        Allowed,

        /// <summary>
        /// Block was out of bounds in the given map.
        /// </summary>
        OutOfBounds,

        /// <summary>
        /// Player was not allowed to place or replace blocks of this particular blocktype.
        /// </summary>
        BlocktypeDenied,

        /// <summary>
        /// Player was not allowed to build on this particular world.
        /// </summary>
        WorldDenied,

        /// <summary>
        /// Player was not allowed to build in this particular zone.
        /// Use World.Map.FindDeniedZone() to find the specific zone.
        /// </summary>
        ZoneDenied,

        /// <summary>
        /// Player's rank is not allowed to build or delete in general.
        /// </summary>
        RankDenied,

        /// <summary>
        /// A plugin callback cancelled block placement/deletion.
        /// To keep player's copy of the map in sync, he will be resent the old blocktype at that location.
        /// </summary>
        PluginDenied,

        /// <summary>
        /// A plugin callback cancelled block placement/deletion.
        /// A copy of the old block will not be sent to the player (he may go out of sync).
        /// </summary>
        PluginDeniedNoUpdate,

        Revert
    }
}
