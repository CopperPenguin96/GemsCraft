using System;
using System.Collections.Generic;
using GemsCraft.Players;
using GemsCraft.Utils;
using JetBrains.Annotations;

namespace GemsCraft.AppSystem.Events
{
    public sealed class SearchingForPlayerEventArgs : EventArgs, IPlayerEvent
    {
        internal SearchingForPlayerEventArgs([CanBeNull] Player player, [NotNull] string searchTerm, List<Player> matches)
        {
            Player = player;
            SearchTerm = searchTerm ?? throw new ArgumentNullException(nameof(searchTerm));
            Matches = matches;
        }

        [CanBeNull]
        public Player Player { get; private set; }
        public string SearchTerm { get; private set; }
        public List<Player> Matches { get; set; }

        public bool CheckVisibility => Player != null;
    }
}
