using System;
using System.Collections.Generic;
using System.Text;
using GemsCraft.Configuration;

namespace GemsCraft.ChatSystem
{
    public class ChatColor
    {
        public const string Black = "&0";
        public const string Navy = "&1";
        public const string Green = "&2";
        public const string Cyan = "&3";
        public const string Maroon = "&4";
        public const string Purple = "&5";
        public const string Gold = "&6";
        public const string Silver = "&7";
        public const string Gray = "&8";
        public const string Blue = "&9";
        public const string Lime = "&a";
        public const string Aqua = "&b";
        public const string Red = "&c";
        public const string Pink = "&d";
        public const string Yellow = "&e";
        public const string White = "&f";

        public static string Sys => Config.Current.SystemColor;
        public static string Help => Config.Current.HelpColor;
        public static string Say => Config.Current.SayColor;
        public static string Announcement => Config.Current.AnnouncementColor;
        public static string PM => Config.Current.PrivateChatColor;
        public static string IRC => Config.Current.IRCColor;
        public static string Me => Config.Current.MeColor;
        public static string Warning => Config.Current.WarningColor;
        public static string Global => Config.Current.GlobalChatColor;
        public static ConsoleColor ToConsoleColor(string mccolor)
        {
            switch (mccolor)
            {
                case Black: return ConsoleColor.Black;
                case Blue: return ConsoleColor.Blue;
                case Aqua: return ConsoleColor.Cyan;
                case Navy: return ConsoleColor.DarkBlue;
                case Cyan: return ConsoleColor.DarkCyan;
                case Gray: return ConsoleColor.DarkGray;
                case Green: return ConsoleColor.DarkGreen;
                case Purple: return ConsoleColor.DarkMagenta;
                case Maroon: return ConsoleColor.DarkRed;
                case Gold: return ConsoleColor.DarkYellow;
                case Silver: return ConsoleColor.Gray;
                case Lime: return ConsoleColor.Green;
                case Pink: return ConsoleColor.Magenta;
                case Red: return ConsoleColor.Red;
                case Yellow: return ConsoleColor.Yellow;
                default: return ConsoleColor.White;
            }
        }

        public static string FromConsoleColor(ConsoleColor cc)
        {
            switch (cc)
            {
                case ConsoleColor.Black: return Black;
                case ConsoleColor.Blue: return Blue;
                case ConsoleColor.Cyan: return Aqua;
                case ConsoleColor.DarkBlue: return Navy;
                case ConsoleColor.DarkCyan: return Cyan;
                case ConsoleColor.DarkGray: return Gray;
                case ConsoleColor.DarkGreen: return Green;
                case ConsoleColor.DarkMagenta: return Purple;
                case ConsoleColor.DarkRed: return Maroon;
                case ConsoleColor.DarkYellow: return Gold;
                case ConsoleColor.Gray: return Silver;
                case ConsoleColor.Green: return Lime;
                case ConsoleColor.Magenta: return Pink;
                case ConsoleColor.Red: return Red;
                case ConsoleColor.Yellow: return Yellow;
                default: return White; // When in doubt, show white.
            }
        }

        public static readonly SortedList<char, string> ColorNames = new SortedList<char, string>
        {
            { '0', "black" },
            { '1', "navy" },
            { '2', "green" },
            { '3', "teal" },
            { '4', "maroon" },
            { '5', "purple" },
            { '6', "olive" },
            { '7', "silver" },
            { '8', "gray" },
            { '9', "blue" },
            { 'a', "lime" },
            { 'b', "aqua" },
            { 'c', "red" },
            { 'd', "magenta" },
            { 'e', "yellow" },
            { 'f', "white" }
        };

        /// <summary>
        /// Gets color name for hex color code.
        /// </summary>
        /// <param name="code">Hexadecimal color code (between 0 and f)</param>
        /// <returns>Lowercase color name</returns>
        public static string GetName(char code)
        {
            code = char.ToLower(code);
            if (IsValidColorCode(code))
            {
                return ColorNames[code];
            }

            string color = Parse(code);
            return color == null ? null : ColorNames[color[1]];
        }

        /// <summary>
        /// Gets color name for a numeric color code.
        /// </summary>
        /// <param name="index">Ordinal numeric color code (between 0 and 15)</param>
        /// <returns>Lowercase color name. If input is out of range, returns null.</returns>
        public static string GetName(int index)
        {
            if (index >= 0 && index <= 15)
            {
                return ColorNames.Values[index];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets color name for a string representation of a color.
        /// </summary>
        /// <param name="color"> Any parsable string representation of a color. </param>
        /// <returns> Lowercase color name.
        /// If input is an empty string, returns empty string.
        /// If input is null or cannot be parsed, returns null. </returns>
        public static string GetName(string color)
        {
            if (color == null)
            {
                return null;
            }
            else if (color.Length == 0)
            {
                return "";
            }
            else
            {
                string parsedColor = Parse(color);
                return parsedColor == null ? null : GetName(parsedColor[1]);
            }
        }

        /// <summary>
        /// Parses a string to a format readable by Minecraft clients. 
        /// an accept color names and color codes
        /// (with or without the ampersand).
        /// </summary>
        /// <param name="code"> Color code character. </param>
        /// <returns> Two-character color string, readable by Minecraft client.
        /// If input is null or cannot be parsed, returns null. </returns>
        public static string Parse(char code)
        {
            code = char.ToLower(code);
            if (IsValidColorCode(code))
            {
                return "&" + code;
            }

            switch (code)
            {
                case 's': return Config.Current.SystemColor;
                case 'y': return Config.Current.SayColor;
                case 'p': return Config.Current.PrivateChatColor;
                case 'r': return Config.Current.AnnouncementColor;
                case 'h': return Config.Current.HelpColor;
                case 'w': return Config.Current.WarningColor;
                case 'm': return Config.Current.MeColor;
                case 'i': return Config.Current.IRCColor;
                case 'g': return Config.Current.GlobalChatColor;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Parses a numeric color code to a string readable by Minecraft clients
        /// </summary>
        /// <param name="index"> Ordinal numeric color code (between 0 and 15). </param>
        /// <returns> Two-character color string, readable by Minecraft client.
        /// If input cannot be parsed, returns null. </returns>
        public static string Parse(int index)
        {
            if (index >= 0 && index <= 15)
            {
                return "&" + ColorNames.Keys[index];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Parses a string to a format readable by Minecraft clients. 
        /// an accept color names and color codes
        /// (with or without the ampersand).
        /// </summary>
        /// <param name="color"> Ordinal numeric color code (between 0 and 15). </param>
        /// <returns> Two-character color string, readable by Minecraft client.
        /// If input is an empty string, returns empty string.
        /// If input is null or cannot be parsed, returns null. </returns>
        public static string Parse(string color)
        {
            if (color == null)
            {
                return null;
            }
            color = color.ToLower();
            switch (color.Length)
            {
                case 2:
                    if (color[0] == '&' && IsValidColorCode(color[1]))
                    {
                        return color;
                    }
                    break;

                case 1:
                    return Parse(color[0]);

                case 0:
                    return "";
            }
            if (ColorNames.ContainsValue(color))
            {
                return "&" + ColorNames.Keys[ColorNames.IndexOfValue(color)];
            }
            else
            {
                return null;
            }
        }

        public static int ParseToIndex(string color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));
            color = color.ToLower();
            if (color.Length == 2 && color[0] == '&')
            {
                if (ColorNames.ContainsKey(color[1]))
                {
                    return ColorNames.IndexOfKey(color[1]);
                }
                else
                {
                    switch (color)
                    {
                        case "&s": return ColorNames.IndexOfKey(Sys[1]);
                        case "&y": return ColorNames.IndexOfKey(Say[1]);
                        case "&p": return ColorNames.IndexOfKey(PM[1]);
                        case "&r": return ColorNames.IndexOfKey(Announcement[1]);
                        case "&h": return ColorNames.IndexOfKey(Help[1]);
                        case "&w": return ColorNames.IndexOfKey(Warning[1]);
                        case "&m": return ColorNames.IndexOfKey(Me[1]);
                        case "&i": return ColorNames.IndexOfKey(IRC[1]);
                        case "&g": return ColorNames.IndexOfKey(Global[1]);
                        default: return 15;
                    }
                }
            }
            else if (ColorNames.ContainsValue(color))
            {
                return ColorNames.IndexOfValue(color);
            }
            else
            {
                return 15; // white
            }
        }

        /// <summary>
        /// Checks whether a color code is valid (checks if it's hexadecimal char).
        /// </summary>
        /// <returns>True is char is valid, otherwise false</returns>
        public static bool IsValidColorCode(char code)
        {
            return (code >= '0' && code <= '9') || (code >= 'a' && code <= 'f') || (code >= 'A' && code <= 'F');
        }

        /// <summary>
        /// GemsCraft uses % and & chars to represent colors,
        /// swtiches % to & so the client knows
        /// </summary>
        public static void ReplacePercentCodes(StringBuilder sb)
        {
            if (sb == null) throw new ArgumentNullException(nameof(sb));
            sb.Replace("%0", "&0");
            sb.Replace("%1", "&1");
            sb.Replace("%2", "&2");
            sb.Replace("%3", "&3");
            sb.Replace("%4", "&4");
            sb.Replace("%5", "&5");
            sb.Replace("%6", "&6");
            sb.Replace("%7", "&7");
            sb.Replace("%8", "&8");
            sb.Replace("%9", "&9");
            sb.Replace("%a", "&a");
            sb.Replace("%b", "&b");
            sb.Replace("%c", "&c");
            sb.Replace("%d", "&d");
            sb.Replace("%e", "&e");
            sb.Replace("%f", "&f");
            sb.Replace("%A", "&a");
            sb.Replace("%B", "&b");
            sb.Replace("%C", "&c");
            sb.Replace("%D", "&d");
            sb.Replace("%E", "&e");
            sb.Replace("%F", "&f");
        }

        public static string ReplacePercentCodes(string message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));
            StringBuilder sb = new StringBuilder(message);
            ReplacePercentCodes(sb);
            return sb.ToString();
        }

        public static string SubstituteSpecialColors(string input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            StringBuilder sb = new StringBuilder(input);
            for (int i = sb.Length - 1; i > 0; i--)
            {
                if (sb[i - 1] != '&') continue;
                switch (char.ToLower(sb[i]))
                {
                    case 's': sb[i] = Sys[1]; break;
                    case 'y': sb[i] = Say[1]; break;
                    case 'p': sb[i] = PM[1]; break;
                    case 'r': sb[i] = Announcement[1]; break;
                    case 'h': sb[i] = Help[1]; break;
                    case 'w': sb[i] = Warning[1]; break;
                    case 'm': sb[i] = Me[1]; break;
                    case 'i': sb[i] = IRC[1]; break;
                    case 'g': sb[i] = Global[1]; break;
                    default:
                        if (!IsValidColorCode(sb[i]))
                        {
                            sb.Remove(i - 1, 1);
                        }
                        break;
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Strips Minecraft color codes from a given string.
        /// Removes all ampersand-character sequences, including standard, fCraft-specific color codes, and newline codes.
        /// Does not remove percent-color-codes.
        /// </summary>
        /// <param name="message"> String to process. </param>
        /// <returns> A processed string. </returns>
        /// <exception cref="ArgumentNullException"> message is null. </exception>
        public static string StripColors(string message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));
            int start = message.IndexOf('&');
            if (start == -1)
            {
                return message;
            }
            int lastInsert = 0;
            StringBuilder output = new StringBuilder(message.Length);
            while (start != -1)
            {
                output.Append(message, lastInsert, start - lastInsert);
                lastInsert = Math.Min(start + 2, message.Length);
                start = message.IndexOf('&', lastInsert);
            }
            output.Append(message, lastInsert, message.Length - lastInsert);
            return output.ToString();
        }


    }
}
