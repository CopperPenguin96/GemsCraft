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

        [StringDescriptor(
            @"The name of the server. 
Used for plugins the server itself. 
Does nothing with the client.",
        MinLength = 1, MaxLength = 64)]
        public string ServerName { get; internal set; } = "[GemsCraft] Default";
        
        [ConfigDescriptor("Whether external players can connect")]
        public bool OnlineMode { get; internal set; } = true;

        [ConfigDescriptor("Icon shown in the Minecraft server list")]
        public ServerIcon Icon { get; internal set; }

        [StringDescriptor("What to display on the server list on the " +
                          "Minecraft client.",
            MinLength = 1, MaxLength = 64)]
        public string MOTD { get; internal set; } = "Welcome to the Server!";

        [IntDescriptor("Max number of players on the server.",
            MinValue = 1, MaxValue = 3000)]
        public int MaxPlayers { get; internal set; } = 20;

        [ConfigDescriptor("Set difficulty for all of the server.")]
        public Difficulty Difficulty { get; internal set; } = Difficulty.Easy;

        [StringDescriptor("The website for the server.",
            MinLength = 1, MaxLength = 64)]
        public string Website { get; internal set; } = string.Empty;
    }
}
