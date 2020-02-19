using System;
using GemBlocks.Worlds;
using GemsCraft.Players;
using GemsCraft.Utils;
using GemsCraft.Worlds;
using JetBrains.Annotations;

namespace GemsCraft.AppSystem.Events.Players
{
    public sealed class PlayerJoinedWorldEventArgs : EventArgs, IPlayerEvent
    {
        public PlayerJoinedWorldEventArgs([NotNull] Player player, World oldWorld, World newWorld, WorldChangeReason reason)
        {
            Player = player ?? throw new ArgumentNullException(nameof(player));
            OldWorld = oldWorld;
            NewWorld = newWorld;
            Reason = reason;
        }

        [NotNull]
        public Player Player { get; }
        public World OldWorld { get; }
        public World NewWorld { get; }
        public WorldChangeReason Reason { get; }
    }
}
