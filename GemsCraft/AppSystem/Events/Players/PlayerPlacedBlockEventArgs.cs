using System;
using GemBlocks.Blocks;
using GemsCraft.Players;
using GemsCraft.Utils;
using GemsCraft.Worlds;
using java.util;
using JetBrains.Annotations;

namespace GemsCraft.AppSystem.Events.Players
{
    public class PlayerPlacedBlockEventArgs : EventArgs, IPlayerEvent
    {
        internal PlayerPlacedBlockEventArgs([NotNull] Player player, [NotNull] Map map, Vector3I coords,
            Block oldBlock, Block newBlock, BlockChangeContext context)
        {
            Player = player;
            Map = map ?? throw new ArgumentNullException(nameof(map));
            Coords = coords;
            OldBlock = oldBlock;
            NewBlock = newBlock;
            Context = context;
        }


        [NotNull]
        public Player Player { get; }

        [NotNull]
        public Map Map { get; }

        public Vector3I Coords { get; }
        public Block OldBlock { get; }
        public Block NewBlock { get; }
        public BlockChangeContext Context { get; }
    }
}
