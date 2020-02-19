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
        [ConfigDescriptor("Whether or not to compress packets send to/from the client.")]
        public bool EnablePacketCompression { get; internal set; } = false;

        [ConfigDescriptor("Show extra debug information on console.")]
        public bool ShowAdvancedDebugInfo { get; internal set; } = false;

        [ConfigDescriptor("Keep the server alive... like a zombie.")]
        public bool HeartbeatSaverEnabled { get; internal set; } = false;

        [ConfigDescriptor("Require a reason to use /ban. Usage: /ban {player} {reason}")]
        public bool RequireBanReason { get; internal set; } = false;

        [ConfigDescriptor("Require a reason to use /rank. Usage: /rank {player} {rank} {reason}")]
        public bool RequireRankChangeReason { get; internal set; } = false;

        [ConfigDescriptor("Require a reason to use /kick. Usage: /rank {player} {reason}")]
        public bool RequireKickReason { get; internal set; } = false;

        [ConfigDescriptor("Whether or no to announce why a player was kicked/banned.")]
        public bool AnnounceKickAndBanReasons { get; internal set; } = true;

        [ConfigDescriptor("Whether or not to send crash reports to the developer.")]
        public bool SubmitCrashReports { get; internal set; } = false;

        [ConfigDescriptor("Whether or not to check for GemsCraft updates.")]
        public bool CheckForUpdates { get; internal set; } = true;
    }
}
