using System;
using GemsCraft.AppSystem.Types;
using GemsCraft.Players;
using GemsCraft.Utils;
using JetBrains.Annotations;

namespace GemsCraft.AppSystem.Events.Players
{
    public sealed class PlayerMovingEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
    {
        internal PlayerMovingEventArgs([NotNull] Player player, Position newPos)
        {
            Player = player ?? throw new ArgumentNullException(nameof(player));
            OldPosition = player.Position;
            NewPosition = newPos;
        }

        [NotNull]
        public Player Player { get; private set; }
        public Position OldPosition { get; private set; }
        public Position NewPosition { get; set; }
        public bool Cancel { get; set; }
    }
}
