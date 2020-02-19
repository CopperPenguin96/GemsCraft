using System;
using GemBlocks.Worlds;
using GemsCraft.Players;
using GemsCraft.Utils;
using GemsCraft.Worlds;
using JetBrains.Annotations;

namespace GemsCraft.AppSystem.Events.Players
{
    public sealed class PlayerJoiningWorldEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
    {
        internal PlayerJoiningWorldEventArgs([NotNull] Player player, [CanBeNull] World oldWorld,
            [NotNull] World newWorld, WorldChangeReason reason,
            string textLine1, string textLine2)
        {
            Player = player ?? throw new ArgumentNullException(nameof(player));
            OldWorld = oldWorld;
            NewWorld = newWorld ?? throw new ArgumentNullException(nameof(newWorld));
            Reason = reason;
            TextLine1 = textLine1;
            TextLine2 = textLine2;
        }

        [NotNull]
        public Player Player { get; }

        [CanBeNull]
        public World OldWorld { get; }

        [NotNull]
        public World NewWorld { get; }

        public WorldChangeReason Reason { get; }
        public string TextLine1 { get; set; }
        public string TextLine2 { get; set; }
        public bool Cancel { get; set; }
    }
}
