using System;
using GemsCraft.Players;
using GemsCraft.Utils;
using JetBrains.Annotations;

namespace GemsCraft.AppSystem.Events.Players
{
    public class PlayerKickedEventArgs : EventArgs, IPlayerEvent
    {
        internal PlayerKickedEventArgs([NotNull] Player player, [NotNull] Player kicker, [CanBeNull] string reason,
            bool announce, bool recordToPlayerDb, LeaveReason context)
        {
            Player = player ?? throw new ArgumentNullException(nameof(player));
            Kicker = kicker ?? throw new ArgumentNullException(nameof(kicker));
            Reason = reason;
            Announce = announce;
            RecordToPlayerDb = recordToPlayerDb;
            Context = context;
        }

        /// <summary> Player who is being kicked. </summary>
        [NotNull]
        public Player Player { get; }

        /// <summary> Player who kicked. </summary>
        [NotNull]
        public Player Kicker { get; protected set; }

        /// <summary> Given kick reason (may be blank). </summary>
        [CanBeNull]
        public string Reason { get; protected set; }

        /// <summary> Whether the kick should be announced in-game and on IRC. </summary>
        public bool Announce { get; }

        /// <summary> Whether kick should be added to the target's record. </summary>
        public bool RecordToPlayerDb { get; protected set; }

        /// <summary> Circumstances that resulted in a kick (e.g. Kick, Ban, BanIP, IdleKick, etc). </summary>
        public LeaveReason Context { get; protected set; }
    }
}
