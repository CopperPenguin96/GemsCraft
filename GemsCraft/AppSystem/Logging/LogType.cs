using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemsCraft.AppSystem.Logging
{
    /// <summary>
    /// Category of a log event.
    /// </summary>
    public enum LogType
    {
        /// <summary>
        /// System activity (loading/saving of data, shutdown and
        /// startup events, etc.)
        /// </summary>
        SystemActivity,

        /// <summary>
        /// When a user has changed worlds.
        /// </summary>
        ChangedWorld,

        /// <summary>
        /// Warnings (missing files, config issues, minor recoverable
        /// issues).
        /// </summary>
        Warning,

        /// <summary>
        /// Recoverable errors (loading/saving problems, connection,
        /// etc.)
        /// </summary>
        Error,

        /// <summary>
        /// Critical non-recoverable issues and crashes.
        /// </summary>
        SeriousError,

        /// <summary>
        /// Routine user activity (commands, kicks, bans, etc.)
        /// </summary>
        UserActivity,

        /// <summary>
        /// Raw commands entered by the player
        /// </summary>
        UserCommand,

        /// <summary>
        /// Permission and hack related activity
        /// </summary>
        SuspiciousActivity,

        /// <summary>
        /// Normal (white) chat written by players.
        /// </summary>
        GlobalChat,

        /// <summary>
        /// Private messages exchanged by players.
        /// </summary>
        PrivateChat,

        /// <summary>
        /// Rank chat messages.
        /// </summary>
        RankChat,

        /// <summary>
        /// Messages and commands entered from console.
        /// </summary>
        ConsoleInput,

        /// <summary>
        /// Messages printed to the console
        /// </summary>
        ConsoleOutput,

        /// <summary>
        /// Messages related to IRC activity.
        /// Does not include all messages relayed to and from.
        /// </summary>
        IRC,

        /// <summary>
        /// Information useful for debugging (details, events, system)
        /// </summary>
        Debug,

        /// <summary>
        /// Special-purpose messages related to event tracing.
        /// </summary>
        Trace,

        /// <summary>
        /// Messages relayed to/from Discord channels.
        /// </summary>
        Discord
    }
}
