using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.ChatSystem;

namespace GemsCraft.Configuration.Categories
{
    public sealed class ChatConfig
    {
        [ConfigDescriptor("Showing the rank with its respective color in the chat")]
        public bool RankColorsInChat { get; internal set; } = true;

        [ConfigDescriptor("Showing the rank with its respective prefix in the chat")]
        public bool RankPrefixesInChat { get; internal set; } = false;

        [IntDescriptor("Max caps the average player can type.",
            MinValue = 1, MaxValue = 12)]
        public int MaxCaps { get; internal set; } = 5;

        [ConfigDescriptor("Enable/Disable the custom chat channel")]
        public bool EnableCustomChat { get; internal set; } = false;

        [StringDescriptor("The name of the custom chat channel.",
            MinLength = 1, MaxLength = 12)]
        public string CustomChatChannel { get; internal set; } = "GemChat";

        [StringDescriptor("The name of the chat alias.",
            MinLength = 1, MaxLength = 12)]
        public string CustomChatAlias { get; internal set; } = "GC";

        [StringDescriptor("What to replace no-no's with.",
            MinLength = 1, MaxLength = 12)]
        public string SwearName { get; internal set; } = "&cDonkey";

        #region Colors

        public string IRCColor { get; internal set; } = ChatColor.Purple;
        public string PrivateChatColor { get; internal set; } = ChatColor.Purple;
        public string DiscordColor { get; internal set; } = ChatColor.Purple;
        public string ErrorColor { get; internal set; } = ChatColor.Red;
        public string SeriousErrorColor { get; internal set; } = ChatColor.Maroon;
        public string RankChatColor { get; internal set; } = ChatColor.Navy;
        public string GlobalChatColor { get; internal set; } = ChatColor.Green;
        public string DefaultColor { get; internal set; } = ChatColor.White;
        public string SayColor { get; internal set; } = ChatColor.Aqua;
        public string AnnouncementColor { get; internal set; } = ChatColor.Gold;
        public string HelpColor { get; internal set; } = ChatColor.Green;
        public string MeColor { get; internal set; } = ChatColor.Lime;

        #endregion
    }
}
