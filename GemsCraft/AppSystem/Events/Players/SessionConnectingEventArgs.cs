using System;
using System.Net;
using GemsCraft.Utils;
using JetBrains.Annotations;

namespace GemsCraft.AppSystem.Events.Players
{
    public sealed class SessionConnectingEventArgs : EventArgs, ICancellableEvent
    {
        internal SessionConnectingEventArgs([NotNull] IPAddress ip)
        {
            IP = ip ?? throw new ArgumentNullException(nameof(ip));
        }

        [NotNull]
        public IPAddress IP { get; }
        public bool Cancel { get; set; }
    }
}
