using System;
using GemBlocks.Worlds;
using GemsCraft.Players;
using GemsCraft.Utils;
using JetBrains.Annotations;

namespace GemsCraft.AppSystem.Events.Players
{
    public sealed class PlayerConnectedEventArgs : EventArgs, IPlayerEvent
    {
        internal PlayerConnectedEventArgs([NotNull] Player player, World startingWorld)
        {
            Player = player ?? throw new ArgumentNullException(nameof(player));
            StartingWorld = startingWorld;
        }

        [NotNull]
        public Player Player { get; private set; }
        public World StartingWorld { get; set; }
    }
}
