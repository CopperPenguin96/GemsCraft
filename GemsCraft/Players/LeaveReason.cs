using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemsCraft.Players
{
    /// <summary>
    /// List of possible reasons for players leaving the server.
    /// </summary>
    public enum LeaveReason : byte
    {
        /// <summary>
        /// Unknown leave reason (default)
        /// </summary>
        Unknown = 0x00,

        /// <summary>
        /// Client exited normally
        /// </summary>
        ClientQuit = 0x01,

        /// <summary>
        /// Client reconnected before old session timed out, or connected from another IP.
        /// </summary>
        ClientReconnect = 0x03,

        /// <summary>
        /// Manual or miscellaneous kick
        /// </summary>
        Kick = 0x10,

        /// <summary>
        /// Kicked for being idle/AFK.
        /// </summary>
        IdleKick = 0x11,

        /// <summary>
        /// Invalid characters in a message
        /// </summary>
        InvalidMessageKick = 0x12,

        /// <summary>
        /// Attempted to place invalid blocktype
        /// </summary>
        [Obsolete]
        InvalidSetTileKick = 0x13,

        /// <summary>
        /// Unknown opcode or packet
        /// </summary>
        InvalidOpcodeKick = 0x14,

        /// <summary>
        /// Triggered antigrief / block spam
        /// </summary>
        BlockSpamKick = 0x15,

        /// <summary>
        /// Kicked for message spam (after warnings)
        /// </summary>
        MessageSpamKick = 0x16,

        /// <summary>
        /// Banned directly by name
        /// </summary>
        Ban = 0x20,

        /// <summary>
        /// Banned indirectly by /BanIP
        /// </summary>
        BanIP = 0x21,

        /// <summary>
        /// Banned indirectly by /BanAll
        /// </summary>
        BanAll = 0x22,


        /// <summary>
        /// Server-side error (uncaught exception in session's thread)
        /// </summary>
        ServerError = 0x30,

        /// <summary>
        /// Server is shutting down
        /// </summary>
        ServerShutdown = 0x31,

        /// <summary>
        /// Server was full or became full
        /// </summary>
        ServerFull = 0x32,

        /// <summary>
        /// World was full (forced join failed)
        /// </summary>
        WorldFull = 0x33,


        /// <summary>
        /// Login failed due to protocol violation/mismatch
        /// </summary>
        ProtocolViolation = 0x41,

        /// <summary>
        /// Login failed due to unverified player name
        /// </summary>
        UnverifiedName = 0x42,

        /// <summary>
        /// Login denied for some other reason
        /// </summary>
        LoginFailed = 0x43,

        /// <summary>
        /// When a player ragequits from the server
        /// </summary>
        RageQuit = 0x44,
    }
}
