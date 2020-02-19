using GemsCraft.Players;
using GemsCraft.Utils;
using JetBrains.Annotations;

namespace GemsCraft.AppSystem.Events.Players
{
    public sealed class PlayerBeingKickedEventArgs : PlayerKickedEventArgs, ICancellableEvent
    {
        internal PlayerBeingKickedEventArgs([NotNull] Player player, [NotNull] Player kicker, [CanBeNull] string reason,
            bool announce, bool recordToPlayerDb, LeaveReason context)
            : base(player, kicker, reason, announce, recordToPlayerDb, context)
        {
        }

        public bool Cancel { get; set; }
    }
}
