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
        public bool RankColorsInChat { get; internal set; } = true;
        public bool RankPrefixesInChat { get; internal set; } = false;
        public int MaxCaps { get; internal set; } = 5;

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
