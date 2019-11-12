using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.Chat;

namespace GemsCraft.AppSystem.Types
{
    public class OptUuid
    {
        public bool Enabled = false;

        private string _cht;

        public string Uuid
        {
            get => Enabled ? _cht : null;
            set => _cht = value;
        }

        public OptUuid(bool enabled, string uuid)
        {
            Enabled = enabled;
            Uuid = uuid;
        }
    }
}
