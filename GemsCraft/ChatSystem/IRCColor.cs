using System;
using System.Collections.Generic;
using System.Text;

namespace GemsCraft.ChatSystem
{
    public sealed class IRCColor
    {
        public const string IRCReset = "\u0003\u000f";
        public const string IRCBold = "\u0002";

        public static readonly int White = 0;
        public static readonly int Black = 1;
        public static readonly int Navy = 2;
        public static readonly int Green = 3;
        public static readonly int Red = 4;
        public static readonly int Maroon = 5;
        public static readonly int Purple = 6;
        public static readonly int Olive = 7;
        public static readonly int Yellow = 8;
        public static readonly int Lime = 9;
        public static readonly int Teal = 10;
        public static readonly int Aqua = 11;
        public static readonly int Blue = 12;
        public static readonly int Magenta = 13;
        public static readonly int Gray = 14;
        public static readonly int Silver = 15;

        public int Value { get; private set; }

        public static explicit operator IRCColor(int val)
        {
            IRCColor color = new IRCColor
            {
                Value = val
            };
            return color;

        }

        public static string ReplaceChatColors(StringBuilder sb)
        {
            
            if (sb == null) throw new ArgumentNullException(nameof(sb));
            SubstituteSpecialColors(sb);

            foreach (var codePair in Colors)
            {
                sb.Replace(codePair.Value, codePair.Key);
            }

            return sb.ToString();
        }

        public static string ReplaceChatColors(string input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            StringBuilder sb = new StringBuilder(input);
            return ReplaceChatColors(sb);
        }

        private static readonly Dictionary<string, string> Colors = new Dictionary<string, string> {
            { ChatColor.White, "\u000300" },
            { ChatColor.Black, "\u000301" },
            { ChatColor.Navy, "\u000302" },
            { ChatColor.Green, "\u000303" },
            { ChatColor.Red, "\u000304" },
            { ChatColor.Maroon, "\u000305" },
            { ChatColor.Purple, "\u000306" },
            { ChatColor.Gold, "\u000307" },
            { ChatColor.Yellow, "\u000308" },
            { ChatColor.Lime, "\u000309" },
            { ChatColor.Cyan, "\u000310" },
            { ChatColor.Aqua, "\u000311" },
            { ChatColor.Blue, "\u000312" },
            { ChatColor.Pink, "\u000313" },
            { ChatColor.Gray, "\u000314" },
            { ChatColor.Silver, "\u000315" },
        };

        public static string EscapeAmpersands(string input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            return input.Replace("&", "&&");
        }

        public static void SubstituteSpecialColors(StringBuilder sb)
        {
            if (sb == null) throw new ArgumentNullException(nameof(sb));
            for (int i = sb.Length - 1; i > 0; i--)
            {
                if (sb[i - 1] != '&') continue;
                switch (char.ToLower(sb[i]))
                {
                    case 's':
                        sb[i] = ChatColor.Sys[1];
                        break;
                    case 'y':
                        sb[i] = ChatColor.Say[1];
                        break;
                    case 'p':
                        sb[i] = ChatColor.PM[1];
                        break;
                    case 'r':
                        sb[i] = ChatColor.Announcement[1];
                        break;
                    case 'h':
                        sb[i] = ChatColor.Help[1];
                        break;
                    case 'w':
                        sb[i] = ChatColor.Warning[1];
                        break;
                    case 'm':
                        sb[i] = ChatColor.Me[1];
                        break;
                    case 'i':
                        sb[i] = ChatColor.IRC[1];
                        break;
                    case 'g':
                        sb[i] = ChatColor.Global[1];
                        break;
                    default:
                        if (!ChatColor.IsValidColorCode(sb[i]))
                        {
                            sb.Remove(i - 1, 2);
                        }

                        break;
                }
            }
        }
    }
}
