using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemsCraft.AppSystem.Types
{
    public class OptLocation
    {
        public bool Enabled = false;

        private string _cht;

        public string Location
        {
            get => Enabled ? _cht : null;
            set => _cht = value;
        }

        public OptLocation(bool enabled, string loc)
        {
            Enabled = enabled;
            Location = loc;
        }
    }
}
