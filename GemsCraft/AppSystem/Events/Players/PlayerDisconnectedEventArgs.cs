using System;
using GemsCraft.Players;
using GemsCraft.Utils;
using JetBrains.Annotations;

namespace GemsCraft.AppSystem.Events.Players
{
    public sealed class PlayerDisconnectedEventArgs : EventArgs, IPlayerEvent
    {
        internal PlayerDisconnectedEventArgs([NotNull] Player player, LeaveReason leaveReason, bool isFake)
        {
            Player = player ?? throw new ArgumentNullException(nameof(player));
            LeaveReason = leaveReason;
            IsFake = isFake;
        }

        [NotNull]
        public Player Player { get; }
        public LeaveReason LeaveReason { get; }
        public bool IsFake { get; }
    }
}
