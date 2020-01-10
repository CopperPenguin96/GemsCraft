using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// ReSharper disable InconsistentNaming

namespace GemsCraft.Configuration.Categories
{
    public sealed class IRCConfig
    {
        public bool BotEnabled { get; internal set; } = false;
        public string[] IRCBotChannels { get; internal set; } = { };
    }
}
