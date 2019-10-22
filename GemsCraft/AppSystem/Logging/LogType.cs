using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemsCraft.AppSystem.Logging
{
    public enum LogType
    {
        Normal, System, ChangedWorld, Warning, Error, SeriousError,
        UserActivity, UserCommand, SuspiciousActivity, GlobalChat,
        PrivateChat, RankChat, ConsoleInput, ConsoleOutput, IRC, Debug,
        Trace, Discord
    }
}
