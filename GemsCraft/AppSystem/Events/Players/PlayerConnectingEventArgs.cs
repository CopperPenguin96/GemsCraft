using System;
using GemsCraft.Players;
using GemsCraft.Utils;
using JetBrains.Annotations;

namespace GemsCraft.AppSystem.Events.Players
{
    public sealed class PlayerConnectingEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
    {
        internal PlayerConnectingEventArgs([NotNull] Player player)
        {
            Player = player ?? throw new ArgumentNullException(nameof(player));
        }

        [NotNull]
        public Player Player { get; private set; }
        public bool Cancel { get; set; }
    }
}
