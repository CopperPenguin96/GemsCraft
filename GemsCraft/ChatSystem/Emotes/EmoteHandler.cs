namespace GemsCraft.ChatSystem.Emotes
{
    public class EmoteHandler
    {
        public static string Process(string message)
        {
            foreach (Emote emote in Emotes)
            {
                foreach (string phrase in emote.Keywords)
                {
                    if (message.Contains(phrase))
                    {
                        message = message.Replace(phrase, emote.Item);
                    }
                }
            }

            return message;
        }

        private static readonly Emote Smile = new Emote
        {
            Item = "☺",
            Keywords = new[] { "{smile}", ":)", ":-)" }
        };

        private static readonly Emote Smile2 = new Emote
        {
            Item = "☻",
            Keywords = new[] { "{smile2}", ":D", ":-D" }
        };

        private static readonly Emote Heart = new Emote
        {
            Item = "♥",
            Keywords = new[] { "{heart}", "{hearts}", "{love}", "<3" }
        };

        private static readonly Emote Diamond = new Emote
        {
            Item = "♦",
            Keywords = new[] { "{diamond}", "{ore}", "{<>}" }
        };

        private static readonly Emote Club = new Emote
        {
            Item = "♣",
            Keywords = new[] { "{club}", "{lucky}" }
        };

        private static readonly Emote Spade = new Emote
        {
            Item = "♠",
            Keywords = new[] { "{spade}" }
        };

        private static readonly Emote Male = new Emote
        {
            Item = "♂",
            Keywords = new[] { "{male}", "{boy}", "{man}" }
        };

        private static readonly Emote Female = new Emote
        {
            Item = "♀",
            Keywords = new[] { "{female}", "{girl}", "{woman}" }
        };

        private static readonly Emote Ta = new Emote
        {
            Item = "♪",
            Keywords = new[] { "{ta}" }
        };

        private static readonly Emote TaTa = new Emote
        {
            Item = "♫",
            Keywords = new[] { "{tata}" }
        };

        private static readonly Emote Sun = new Emote
        {
            Item = "☼",
            Keywords = new[] { "sun" }
        };

        private static readonly Emote Exclamation = new Emote
        {
            Item = "‼",
            Keywords = new[] { "{omg}", "{!!}" }
        };

        private static readonly Emote Paragraph = new Emote
        {
            Item = "¶",
            Keywords = new[] { "{paragraph}", "{P}" }
        };

        private static readonly Emote Section = new Emote
        {
            Item = "§",
            Keywords = new[] { "{S}", "{sect}", "{section}" }
        };

        private static readonly Emote Bullet = new Emote
        {
            Item = "•",
            Keywords = new[] { "{boom}", "{*}", "{bull}", "{bullet}", "{dot}" }
        };

        private static readonly Emote Hole = new Emote
        {
            Item = "◘",
            Keywords = new[] { "{wonderland}", "{hole}" }
        };

        private static readonly Emote Circle = new Emote
        {
            Item = "○",
            Keywords = new[] { "{circle}", "{o}" }
        };

        private static readonly Emote Bar = new Emote
        {
            Item = "▬",
            Keywords = new[] { "{bar}", "{line}", "{-}" }
        };

        private static readonly Emote Corner = new Emote
        {
            Item = "∟",
            Keywords = new[] { "{L}", "{angle}", "{corner}" }
        };

        private static readonly Emote House = new Emote
        {
            Item = "⌂",
            Keywords = new[] { "{house}", "{pentagon}" }
        };

        private static readonly Emote Caret = new Emote
        {
            Item = "^",
            Keywords = new[] { "{caret}", "{hat}" }
        };

        private static readonly Emote Wave = new Emote
        {
            Item = "~",
            Keywords = new[] { "{tilde}", "{wave}" }
        };

        private static readonly Emote Grave = new Emote
        {
            Item = "`",
            Keywords = new[] { "{'}", "{grave}" }
        };

        private static readonly Emote Up = new Emote
        {
            Item = "↑",
            Keywords = new[] { "{^}", "{up}" }
        };

        private static readonly Emote Down = new Emote
        {
            Item = "↓",
            Keywords = new[] { "{v}", "{down}" }
        };

        private static readonly Emote Right = new Emote
        {
            Item = "→",
            Keywords = new[] { "{>}", "{->}", "{right}" }
        };

        private static readonly Emote Left = new Emote
        {
            Item = "←",
            Keywords = new[] { "{left}", "{<-}", "{<}" }
        };

        private static readonly Emote Up2 = new Emote
        {
            Item = "▲",
            Keywords = new[] { "{^^}", "{up2}" }
        };

        private static readonly Emote Down2 = new Emote
        {
            Item = "▼",
            Keywords = new[] { "{vv}", "{down2}" }
        };

        private static readonly Emote Right2 = new Emote
        {
            Item = "►",
            Keywords = new[] { "{>>}", "{->>}", "{right2}" }
        };

        private static readonly Emote Left2 = new Emote
        {
            Item = "◄",
            Keywords = new[] { "{left2}", "{<<-}", "{<<}" }
        };

        private static readonly Emote LeftRight = new Emote
        {
            Item = "↔",
            Keywords = new[] { "{leftright}", "{<->}" }
        };

        private static readonly Emote UpDown = new Emote
        {
            Item = "↕",
            Keywords = new[] { "{updown}", "{^v}" }
        };

        private static readonly Emote UpDown2 = new Emote
        {
            Item = "↨",
            Keywords = new[] { "{updown2}", "{^v_}", "{^^vv}" }
        };

        private static readonly Emote[] Emotes = {
            Smile, Smile2, Heart, Diamond, Club, Spade,
            Male, Female, Ta, TaTa, Sun, Exclamation,
            Paragraph, Section, Bullet, Hole, Circle,
            Bar, Corner, House, Caret, Wave, Grave,
            Up, Down, Right, Left,
            Up2, Down2, Right2, Left2,
            LeftRight, UpDown, UpDown2
        };
    }
}
