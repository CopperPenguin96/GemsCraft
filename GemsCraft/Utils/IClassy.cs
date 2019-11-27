﻿namespace GemsCraft.Utils
{
    /// <summary>
    /// Provides a way for printing an object's name beautified with Minecraft color codes.
    /// It was "classy" in a sense that it was colored based on "class" (rank) of a player/world/zone.
    /// </summary>
    public interface IClassy
    {
        string ClassyName { get; }
    }
}
