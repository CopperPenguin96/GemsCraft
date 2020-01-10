using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem;
using GemsCraft.ChatSystem;

namespace GemsCraft.Configuration.Categories
{
    public sealed class LoggingConfig
    {
        public string LogDirectory { get; internal set; } = Files.LogPath;
        public bool SaveLogs { get; internal set; } = true;
        public string SystemColor { get; internal set; } = ChatColor.Gray;
        public string UserActivityColor { get; internal set; } = ChatColor.Gray;
        public string UserCommandColor { get; internal set; } = ChatColor.Gray;
        public string ConsoleInputColor { get; internal set; } = ChatColor.Gray;
        public string ConsoleOutputColor { get; internal set; } = ChatColor.Gray;
        public string TraceColor { get; internal set; } = ChatColor.Gray;
        public string DebugColor { get; internal set; } = ChatColor.Gray;
        public string ChangedWorldColor { get; internal set; } = ChatColor.Lime;
        public string WarningColor { get; internal set; } = ChatColor.Yellow;
        public string SuspiciousActivityColor { get; internal set; } = ChatColor.Yellow;
    }
}
