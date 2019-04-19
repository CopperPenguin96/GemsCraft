﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GemsCraft.Utils
{

    /// <summary> Static class with definitions of Minecraft color codes,
    /// parsers, converters, and utilities. </summary>
    public static class Color
    {
        public const string Black = "&0",
                            Navy = "&1",
                            Green = "&2",
                            Teal = "&3",
                            Maroon = "&4",
                            Purple = "&5",
                            Olive = "&6",
                            Silver = "&7",
                            Gray = "&8",
                            Blue = "&9",
                            Lime = "&a",
                            Aqua = "&b",
                            Red = "&c",
                            Magenta = "&d",
                            Yellow = "&e",
                            White = "&f";

        // User-defined color assignments. Set by Config.ApplyConfig.
        public static string Sys, Help, Say, Announcement, PM, IRC, Me, Custom, Warning, Global;

        // Defaults for user-defined colors.
        public const string SysDefault = Yellow,
                            HelpDefault = Lime,
                            SayDefault = Green,
                            AnnouncementDefault = Green,
                            PMDefault = Aqua,
                            IRCDefault = Purple,
                            MeDefault = Purple,
                            WarningDefault = Red,
                            CustomDefault = Yellow,
                            GlobalDefault = Purple;

        public static readonly SortedList<char, string> ColorNames = new SortedList<char, string>{
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


        /// <summary> Gets color name for hex color code. </summary>
        /// <param name="code"> Hexadecimal color code (between '0' and 'f'). </param>
        /// <returns> Lowercase color name. </returns>
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


        /// <summary> Gets color name for a numeric color code. </summary>
        /// <param name="index"> Ordinal numeric color code (between 0 and 15). </param>
        /// <returns> Lowercase color name. If input is out of range, returns null. </returns>
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


        /// <summary> Gets color name for a string representation of a color. </summary>
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

            if (color.Length == 0)
            {
                return "";
            }

            string parsedColor = Parse(color);
            return parsedColor == null ? null : GetName(parsedColor[1]);
        }



        /// <summary> Parses a string to a format readable by Minecraft clients. 
        /// an accept color names and color codes (with or without the ampersand). </summary>
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
            else
            {
                switch (code)
                {
                    case 's': return Sys;
                    case 'y': return Say;
                    case 'p': return PM;
                    case 'r': return Announcement;
                    case 'h': return Help;
                    case 'w': return Warning;
                    case 'm': return Me;
                    case 'i': return IRC;
                    case 'g': return Global;
                    default:
                        return null;
                }
            }
        }


        /// <summary> Parses a numeric color code to a string readable by Minecraft clients </summary>
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


        /// <summary> Parses a string to a format readable by Minecraft clients. 
        /// an accept color names and color codes (with or without the ampersand). </summary>
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
                        if (IsValidColorCode(sb[i]))
                        {
                            continue;
                        }
                        else
                        {
                            sb.Remove(i - 1, 1);
                        }
                        break;
                }
            }
            return sb.ToString();
        }


        /// <summary> Strips Minecraft color codes from a given string.
        /// Removes all ampersand-character sequences, including standard, fCraft-specific color codes, and newline codes.
        /// Does not remove percent-color-codes. </summary>
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


        #region IRC Colors

        /// <summary> Replaces IRC color codes with equivalent Minecraft color codes, in the given StringBuilder. 
        /// Opposite of MinecraftToIrcColors method. </summary>
        /// <param name="sb"> StringBuilder objects, the contents of which will be processed. </param>
        /// <exception cref="ArgumentNullException"> sb is null. </exception>
        public static void IrcToMinecraftColors(StringBuilder sb)
        {
            if (sb == null) throw new ArgumentNullException(nameof(sb));
            SubstituteSpecialColors(sb);
            foreach (var codePair in MinecraftToIRCColors)
            {
                sb.Replace(codePair.Value, codePair.Key);
            }
        }


        /// <summary> Replaces IRC color codes with equivalent Minecraft color codes, in the given string.
        /// Opposite of MinecraftToIrcColors method. </summary>
        /// <param name="input"> String to process. </param>
        /// <returns> A processed string. </returns>
        /// <exception cref="ArgumentNullException"> input is null. </exception>
        public static string IrcToMinecraftColors(string input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            StringBuilder sb = new StringBuilder(input);
            IrcToMinecraftColors(sb);
            return sb.ToString();
        }

        public const string IRCReset = "\u0003\u000f";
        public const string IRCBold = "\u0002";

        static readonly Dictionary<string, string> MinecraftToIRCColors = new Dictionary<string, string> {
            { White, "\u000300" },
            { Black, "\u000301" },
            { Navy, "\u000302" },
            { Green, "\u000303" },
            { Red, "\u000304" },
            { Maroon, "\u000305" },
            { Purple, "\u000306" },
            { Olive, "\u000307" },
            { Yellow, "\u000308" },
            { Lime, "\u000309" },
            { Teal, "\u000310" },
            { Aqua, "\u000311" },
            { Blue, "\u000312" },
            { Magenta, "\u000313" },
            { Gray, "\u000314" },
            { Silver, "\u000315" },
        };


        public static string EscapeAmpersands(string input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            return input.Replace("&", "&&");
        }


        /// <summary> Replaces Minecraft color codes with equivalent IRC color codes, in the given StringBuilder.
        /// Opposite of IrcToMinecraftColors method. </summary>
        /// <param name="sb"> StringBuilder objects, the contents of which will be processed. </param>
        /// <exception cref="ArgumentNullException"> sb is null. </exception>
        public static void MinecraftToIrcColors(StringBuilder sb)
        {
            if (sb == null) throw new ArgumentNullException(nameof(sb));
            SubstituteSpecialColors(sb);
            foreach (var codePair in MinecraftToIRCColors)
            {
                sb.Replace(codePair.Key, codePair.Value);
            }
        }


        /// <summary> Replaces Minecraft color codes with equivalent IRC color codes, in the given string.
        /// Opposite of IrcToMinecraftColors method. </summary>
        /// <param name="input"> String to process. </param>
        /// <returns> A processed string. </returns>
        /// <exception cref="ArgumentNullException"> input is null. </exception>
        public static string MinecraftToIrcColors(string input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            StringBuilder sb = new StringBuilder(input);
            MinecraftToIrcColors(sb);
            return sb.ToString();
        }
        /// <summary> Substitutes all fCraft-specific ampersand color codes (like &amp;S/Color.Sys)
        /// with the assigned Minecraft colors (like &amp;E/Color.Yellow).
        /// Strips any unrecognized sequences. Does not replace percent-codes.
        /// Note that LineWrapper itself does this substitution internally. </summary>
        /// <param name="sb"> StringBuilder, contents of which will be processed. </param>
        /// <returns> Processed string. </returns>
        /// <exception cref="ArgumentNullException"> sb is null. </exception>
        public static void SubstituteSpecialColors(StringBuilder sb)
        {
            if (sb == null) throw new ArgumentNullException(nameof(sb));
            for (int i = sb.Length - 1; i > 0; i--)
            {
                if (sb[i - 1] != '&') continue;
                switch (char.ToLower(sb[i]))
                {
                    case 's':
                        sb[i] = Sys[1];
                        break;
                    case 'y':
                        sb[i] = Say[1];
                        break;
                    case 'p':
                        sb[i] = PM[1];
                        break;
                    case 'r':
                        sb[i] = Announcement[1];
                        break;
                    case 'h':
                        sb[i] = Help[1];
                        break;
                    case 'w':
                        sb[i] = Warning[1];
                        break;
                    case 'm':
                        sb[i] = Me[1];
                        break;
                    case 'i':
                        sb[i] = IRC[1];
                        break;
                    case 'g':
                        sb[i] = Global[1];
                        break;
                    default:
                        if (!IsValidColorCode(sb[i]))
                        {
                            sb.Remove(i - 1, 2);
                        }
                        break;
                }
            }
        }



        enum IRCColor
        {
            White = 0,
            Black,
            Navy,
            Green,
            Red,
            Maroon,
            Purple,
            Olive,
            Yellow,
            Lime,
            Teal,
            Aqua,
            Blue,
            Magenta,
            Gray,
            Silver
        }
    }
}
#endregion