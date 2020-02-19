using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace GemsCraft.AppSystem.Events
{
    public sealed class ShutdownEventArgs : EventArgs
    {
        [NotNull]
        public ShutdownParams ShutdownParams { get; }

        internal ShutdownEventArgs([NotNull] ShutdownParams shutdownParams)
        {
            ShutdownParams = shutdownParams ?? throw new ArgumentNullException(nameof(shutdownParams));
        }
    }
}
