using System;
using GemsCraft.Players;
using JetBrains.Annotations;

namespace GemsCraft.AppSystem.Events.Players
{
    public sealed class SessionDisconnectedEventArgs : EventArgs
    {
        internal SessionDisconnectedEventArgs([NotNull] Player player, LeaveReason leaveReason)
        {
            Player = player ?? throw new ArgumentNullException(nameof(player));
            LeaveReason = leaveReason;
        }

        [NotNull]
        public Player Player { get; private set; }
        public LeaveReason LeaveReason { get; private set; }
    }
}
