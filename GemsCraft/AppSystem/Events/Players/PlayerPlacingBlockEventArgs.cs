using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemBlocks.Blocks;
using GemsCraft.Players;
using GemsCraft.Utils;
using GemsCraft.Worlds;
using java.util;
using JetBrains.Annotations;

namespace GemsCraft.AppSystem.Events.Players
{
    public sealed class PlayerPlacingBlockEventArgs : PlayerPlacedBlockEventArgs
    {
        internal PlayerPlacingBlockEventArgs([NotNull] Player player, [NotNull] Map map, Vector3I coords,
            Block oldBlock, Block newBlock, BlockChangeContext context, CanPlaceResult result)
            : base(player, map, coords, oldBlock, newBlock, context)
        {
            Result = result;
        }

        public CanPlaceResult Result { get; set; }
    }
}
