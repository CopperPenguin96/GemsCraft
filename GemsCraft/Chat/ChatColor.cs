using System;
using System.Windows.Forms;

namespace GemsCraft.Chat
{
    public class ChatColor
    {
        public const string Black = "0";
        public const string Navy = "1";
        public const string Green = "2";
        public const string Cyan = "3";
        public const string Maroon = "4";
        public const string Purple = "5";
        public const string Gold = "6";
        public const string Silver = "7";
        public const string Gray = "8";
        public const string Blue = "9";
        public const string Lime = "a";
        public const string Aqua = "b";
        public const string Red = "c";
        public const string Pink = "d";
        public const string Yellow = "e";
        public const string White = "f";

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
    }
}
