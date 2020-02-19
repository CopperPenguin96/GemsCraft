using System;
using GemBlocks.Blocks;
using GemsCraft.Players;
using GemsCraft.Utils;
using JetBrains.Annotations;

namespace GemsCraft.AppSystem.Events.Players
{
    public sealed class PlayerClickedEventArgs : EventArgs, IPlayerEvent
    {
        internal PlayerClickedEventArgs([NotNull] Player player, Vector3I coords, ClickAction action, Block block)
        {
            Player = player ?? throw new ArgumentNullException(nameof(player));
            Coords = coords;
            Block = block;
            Action = action;
        }

        [NotNull]
        public Player Player { get; }
        public Vector3I Coords { get; }
        public Block Block { get; }
        public ClickAction Action { get; }
    }
}
