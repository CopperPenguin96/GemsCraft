using System;
using GemsCraft.Players;
using JetBrains.Annotations;

namespace GemsCraft.AppSystem
{
    /// <summary>
    /// Describes the circumstances of server shutdowns.
    /// </summary>
    public sealed class ShutdownParams
    {
        public ShutdownParams(ShutdownReason reason, TimeSpan delay, bool killProcess, bool restart)
        {
            Reason = reason;
            Delay = delay;
            KillProcess = killProcess;
            Restart = restart;
        }

        public ShutdownParams(ShutdownReason reason, TimeSpan delay, bool killProcess,
            bool restart, [CanBeNull] string customReason, [CanBeNull] Player initiatedBy) :
            this(reason, delay, killProcess, restart)
        {
            _customReasonString = customReason;
            InitiatedBy = initiatedBy;
        }

        public ShutdownReason Reason { get; }

        private readonly string _customReasonString;
        [NotNull]
        public string ReasonString => _customReasonString ?? Reason.ToString();

        /// <summary>
        /// Delay before shutting down.
        /// </summary>
        public TimeSpan Delay { get; }

        /// <summary>
        /// Whether GemsCraft should try to forcefully kill the current process.
        /// </summary>
        public bool KillProcess { get; }

        /// <summary>
        /// Whether the server is expected to restart itself after shutting down.
        /// </summary>
        public bool Restart { get; }

        /// <summary>
        /// Player who initiated the shutdown. May be null or Console.
        /// </summary>
        [CanBeNull]
        public Player InitiatedBy { get; }
    }
}
