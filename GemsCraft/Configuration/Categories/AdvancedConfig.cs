using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem;

namespace GemsCraft.Configuration.Categories
{
    public sealed class AdvancedConfig
    {
        public bool EnablePacketCompression { get; internal set; } = false;
        public string PlayerDBDirectory { get; internal set; } = Files.PlayerDatabasePath;
        public bool ShowAdvancedDebugInfo { get; internal set; } = false;
        public bool HeartbeatSaverEnabled { get; internal set; } = false;
        public bool RequireBanReason { get; internal set; } = false;
        public bool RequireRankChangeReason { get; internal set; } = false;
        public bool RequireKickReason { get; internal set; } = false;
        
    }
}
