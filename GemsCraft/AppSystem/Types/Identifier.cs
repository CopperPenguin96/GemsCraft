using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemsCraft.AppSystem.Types
{
    internal class Identifier
    {
        public string Namespace { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Namespace}:{Name}";
        }
    }
}
