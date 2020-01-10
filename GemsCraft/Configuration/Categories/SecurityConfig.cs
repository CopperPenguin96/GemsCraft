using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemsCraft.Configuration.Categories
{
    public sealed class SecurityConfig
    {
        public bool EnableEncryption { get; internal set; } = true;
    }
}
