using System;
using GemBlocks.Blocks;
using GemsCraft.Players;
using GemsCraft.Utils;
using JetBrains.Annotations;

namespace GemsCraft.AppSystem.Events.Players
{
    public sealed class PlayerClickingEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
    {
        internal PlayerClickingEventArgs([NotNull] Player player, Vector3I coords,
            ClickAction action, Block block)
        {
            Player = player ?? throw new ArgumentNullException(nameof(player));
            Coords = coords;
            Action = action;
            Block = block;
        }

        [NotNull]
        public Player Player { get; private set; }
        public Vector3I Coords { get; set; }
        public Block Block { get; set; }
        /// <summary> Whether the player is building a block (right-click) or deleting it (left-click). </summary>
        public ClickAction Action { get; set; }
        public bool Cancel { get; set; }
    }
}
