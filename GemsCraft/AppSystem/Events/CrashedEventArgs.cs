using System;

namespace GemsCraft.AppSystem.Events
{
    public sealed class CrashedEventArgs : EventArgs
    {
        public string Message { get; }
        public string Location { get; }
        public Exception Exception { get; }
        public bool SubmitCrashReport { get; set; }
        public bool IsCommonProblem { get; }
        public bool ShutdownImminent { get; }

        internal CrashedEventArgs(string message, string location, Exception exception, bool submitCrashReport, bool isCommonProblem, bool shutdownImminent)
        {
            Message = message;
            Location = location;
            Exception = exception;
            SubmitCrashReport = submitCrashReport;
            IsCommonProblem = isCommonProblem;
            ShutdownImminent = shutdownImminent;
        }
    }
}
