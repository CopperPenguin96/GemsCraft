using System;
using GemsCraft.AppSystem.Types;
using GemsCraft.Players;
using GemsCraft.Utils;
using JetBrains.Annotations;

namespace GemsCraft.AppSystem.Events.Players
{
    public sealed class PlayerMovedEventArgs : EventArgs, IPlayerEvent
    {
        internal PlayerMovedEventArgs([NotNull] Player player, Position oldPos)
        {
            Player = player ?? throw new ArgumentNullException(nameof(player));
            OldPosition = oldPos;
            NewPosition = player.Position;
        }

        [NotNull]
        public Player Player { get; private set; }
        public Position OldPosition { get; private set; }
        public Position NewPosition { get; private set; }
    }
}
