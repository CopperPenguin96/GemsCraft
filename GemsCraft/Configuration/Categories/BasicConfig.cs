using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.Worlds;

namespace GemsCraft.Configuration.Categories
{
    public sealed class BasicConfig
    {
        public string ServerName { get; internal set; } = "[GemsCraft] Default";
        public bool OnlineMode { get; internal set; } = true;
        public bool AutoUpdateChecker { get; internal set; } = true;
        public ServerIcon Icon { get; internal set; }
        public string MOTD { get; internal set; } = "Welcome to the Server!";
        public int MaxPlayers { get; internal set; } = 20;
        public Difficulty Difficulty = Difficulty.Easy;
    }
}
