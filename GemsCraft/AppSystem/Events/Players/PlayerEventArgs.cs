using System;
using GemsCraft.Players;
using GemsCraft.Utils;

namespace GemsCraft.AppSystem.Events.Players
{
    public sealed class PlayerEventArgs : EventArgs, IPlayerEvent
    {
        internal PlayerEventArgs(Player player)
        {
            Player = player;
        }

        public Player Player { get; }
    }
}
