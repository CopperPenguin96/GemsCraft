using System;
using System.Diagnostics;
using GemsCraft.AppSystem.Logging;

namespace GemsCraft.AppSystem.Events
{
    public sealed class LogEventArgs : EventArgs
    {
        public string RawMessage { get; }
        public string Message { get; }
        public LogType MessageType { get; }
        public bool WriteToFile { get; }
        public bool WriteToConsole { get; }

        [DebuggerStepThrough]
        internal LogEventArgs(string rawMessage, string message, LogType messageType, bool writeToFile,
            bool writeToConsole)
        {
            RawMessage = rawMessage;
            Message = message;
            MessageType = messageType;
            WriteToFile = writeToFile;
            WriteToConsole = writeToConsole;
        }
    }
}
