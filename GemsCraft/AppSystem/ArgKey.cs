// Copyright 2009-2012 Matvei Stefarov <me@matvei.org>
// Changes Copyright 2020 Alex Potter <CopperPenquin96@gmail.com>

namespace GemsCraft.AppSystem
{
    /// <summary>
    /// Enumerates the recognized command-line switches/arguments.
    /// Args are parsed in Server.InitLibrary.
    /// </summary>
    public enum ArgKey
    {
        /// <summary>
        /// Working path (directory) that GemsCraft shoudl use.
        /// </summary>
        Path,

        /// <summary>
        /// Path (directory) where the log files should be placed.
        /// If the path is relative, it's computed against the working
        /// path.
        /// </summary>
        LogPath,
        
        /// <summary>
        /// Path (directory) where the map files should be placed.
        /// If the path is relative, it's computed against the working
        /// path.
        /// </summary>
        MapPath,

        /// <summary>
        /// Path (file) of the config.
        /// If the path is realtive, it's computed agsint the working
        /// path.
        /// </summary>
        Config,

        /// <summary>
        /// If NoRestart flag is present, GemsCraft will shutdown
        /// isntead of restarting.
        /// </summary>
        NoRestart,

        /// <summary>
        /// If ExitOnCrash flag is present, GemsCraft frontends will
        /// exit at once in the event of an unrecoverable crash.
        /// </summary>
        ExitOnCrash,

        /// <summary>
        /// Disables all logging.
        /// </summary>
        NoLog,

        /// <summary>
        /// Disables colors in CLI frontends.
        /// </summary>
        NoConsoleColor
    }
}
