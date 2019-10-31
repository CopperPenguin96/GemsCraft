using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemsCraft.Worlds
{
    /// <summary>
    /// TODO : possibly inherit Generator to create different world types?
    /// </summary>
    public class Generator
    {
        private World _worldBuilder = new World();
        public Generator(string worldName)
        {
            _worldBuilder.WorldName = worldName;

        }
    }
}
