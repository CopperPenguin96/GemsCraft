using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using GemsCraft.AppSystem;
using GemsCraft.ChatSystem.Emotes;
using GemsCraft.Configuration;
using GemsCraft.Players;
using GemsCraft.Utils;
using JetBrains.Annotations;

namespace GemsCraft.ChatSystem
{
    /// <summary>
    /// Helper class for handling player-generated chat.
    /// </summary>
    public static class Chat
    {
        public static List<string> Swears = new List<string>();
        public static IEnumerable<Regex> BadWordMatchers;

        /// <summary>
        /// Conversion for code page 437 characters from index 0 to 31 to unicode.
        /// </summary>
        public const string ControlCharReplacements = "\0☺☻♥♦♣♠•◘○◙♂♀♪♫☼►◄↕‼¶§▬↨↑↓→←∟↔▲▼";

        /// <summary>
        /// Conversion for code page 437 characters from index 127 to 255 to unicode.
        /// </summary>
        public const string ExtendedCharReplacements = "⌂ÇüéâäàåçêëèïîìÄÅÉæÆôöòûùÿÖÜ¢£¥₧ƒáíóúñÑªº¿⌐¬½¼¡«»" +
                                                       "░▒▓│┤╡╢╖╕╣║╗╝╜╛┐└┴┬├─┼╞╟╚╔╩╦╠═╬╧╨╤╥╙╘╒╓╫╪┘┌" +
                                                       "█▄▌▐▀αßΓπΣσµτΦΘΩδ∞φε∩≡±≥≤⌠⌡÷≈°∙·√ⁿ²■\u00a0";

        private static string FormatMessage(string rawMessage, Player player)
        {
            rawMessage = EmoteHandler.Process(rawMessage);
            rawMessage = rawMessage.Replace("$name", player.ClassyName + "&f");
            rawMessage = rawMessage.Replace("$kicks", player.TimesKickedOthers.ToString());
            rawMessage = rawMessage.Replace("$bans", player.TimesBannedOthers.ToString());
            rawMessage = rawMessage.Replace("$awesome", "It is my professional opinion, that " + Config.Basic.ServerName + " is the best server on Minecraft Java Edition!");
            rawMessage = rawMessage.Replace("$server", Config.Basic.ServerName);
            rawMessage = rawMessage.Replace("$motd", Config.Basic.MOTD);
            rawMessage = rawMessage.Replace("$date", DateTime.UtcNow.ToShortDateString());
            rawMessage = rawMessage.Replace("$time", DateTime.Now.ToString());
            rawMessage = rawMessage.Replace("$money", player.Money.ToString());
            rawMessage = rawMessage.Replace("$mad", "U &cmad&f, bro?");
            rawMessage = rawMessage.Replace("$welcome", "Welcome to " + Config.Basic.ServerName);
            rawMessage = rawMessage.Replace("$clap", "A round of applause might be appropriate, *claps*");
            rawMessage = rawMessage.Replace("$website", Config.Misc.WebsiteUrl);
            rawMessage = rawMessage.Replace("$ws", Config.Misc.WebsiteUrl);

            Player[] active = Server.OnlinePlayers.ToArray();
            if (active.Length > 0)
            {
                int rndPlayer = Server.Random.Next(0, active.Length - 1);
                string dis = active[rndPlayer].DisplayedName ?? active[rndPlayer].Name;
                rawMessage = rawMessage.Replace($"moron", dis + "&c is a complete and total moron.");
            }

            rawMessage = rawMessage.Replace("$irc", Config.IRC.BotEnabled
                ? Config.IRC.IRCBotChannels.ToCommaSeperatedString()
                : "No IRC");

            if (player.Can(Permission.UseColorCodes))
            {
                rawMessage = rawMessage.Replace("$lime", "&a");     //alternate color codes for ease if you can't remember the codes
                rawMessage = rawMessage.Replace("$aqua", "&b");
                rawMessage = rawMessage.Replace("$cyan", "&b");
                rawMessage = rawMessage.Replace("$red", "&c");
                rawMessage = rawMessage.Replace("$magenta", "&d");
                rawMessage = rawMessage.Replace("$pink", "&d");
                rawMessage = rawMessage.Replace("$yellow", "&e");
                rawMessage = rawMessage.Replace("$white", "&f");
                rawMessage = rawMessage.Replace("$navy", "&1");
                rawMessage = rawMessage.Replace("$darkblue", "&1");
                rawMessage = rawMessage.Replace("$green", "&2");
                rawMessage = rawMessage.Replace("$teal", "&3");
                rawMessage = rawMessage.Replace("$maroon", "&4");
                rawMessage = rawMessage.Replace("$purple", "&5");
                rawMessage = rawMessage.Replace("$olive", "&6");
                rawMessage = rawMessage.Replace("$gold", "&6");
                rawMessage = rawMessage.Replace("$silver", "&7");
                rawMessage = rawMessage.Replace("$grey", "&8");
                rawMessage = rawMessage.Replace("$gray", "&8");
                rawMessage = rawMessage.Replace("$blue", "&9");
                rawMessage = rawMessage.Replace("$black", "&0");
                rawMessage = rawMessage.Replace("$obfuscated", "&k");
                rawMessage = rawMessage.Replace("$funky", "&k");
                rawMessage = rawMessage.Replace("$strikethrough", "&m");
                rawMessage = rawMessage.Replace("$strike", "&m");
                rawMessage = rawMessage.Replace("$italics", "&italics");
                rawMessage = rawMessage.Replace("$bold", "&l");
                rawMessage = rawMessage.Replace("$underline", "&n");
                rawMessage = rawMessage.Replace("$reset", "&r");
            }

            if (!player.Can(Permission.ChatWithCaps))
            {
                int caps = 0;
                for (int i = 0; i < rawMessage.Length; i++)
                {
                    if (!char.IsUpper(rawMessage[i])) continue;
                    caps++;
                    if (caps <= Config.Chat.MaxCaps) continue;
                    rawMessage = rawMessage.ToLower();
                    player.Message("Your message was changed to lowercase as it exceeded the max amount of caps");
                }
            }

            if (!player.Can(Permission.Swear))
            {
                if (!File.Exists(Files.SwearPath))
                {
                    StringBuilder sb = new StringBuilder();
                    string[] words = new Uri("http://gemz.christplay.x10host.com/download/swears.txt").GetUrlSourceAsList().ToArray();
                    foreach (string word in words)
                    {
                        sb.Append(word);
                    }
                    File.WriteAllText("SwearWords.txt", sb.ToString());
                }

                string censoredText = ChatColor.ReplacePercentCodes(Config.Misc.SwearName);

                if (Config.Misc.SwearName == null)
                {
                    censoredText = "Chicken";
                }

                const string patternTemplate = @"\b({0})(s?)\b";
                const RegexOptions options = RegexOptions.IgnoreCase;

                if (Swears.Count == 0)
                {
                    Swears.AddRange(File.ReadAllLines("SwearWords.txt").
                        Where(line => line.StartsWith("#") == false || line.Trim().Equals(string.Empty)));
                }

                if (BadWordMatchers == null)
                {
                    BadWordMatchers = Swears.
                        Select(x => new Regex(string.Format(patternTemplate, x), options));
                }

                string output = BadWordMatchers.
                    Aggregate(rawMessage, (current, matcher) => matcher.Replace(current, censoredText));
                rawMessage = output;
            }

            return rawMessage;
        }

        public static bool SendGlobal([NotNull] Player player, [NotNull] string rawMessage)
        {
            if (player == null) throw new ArgumentNullException(nameof(player));

        }
    }
}
