using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemsCraft.Configuration.Categories
{
    public sealed class WorldConfig
    {
        private int _tWorld = 0;
        private int _mWorld
        {
            get => 10;
            set
            {
                if (Config.Basic == null) return;
                else
                {
                    _tWorld = value;
                }
            }
        }

        [IntDescriptor("Max amount of players per world.",
            MinValue = 1, MaxValue = 3000)]
        public int MaxPerWorld
        {
            get => _mWorld;
            internal set
            {
                if (value > Config.Basic.MaxPlayers)
                    throw new ArgumentOutOfRangeException(
                        "Max players per world cannot be bigger than max server players!");
                _mWorld = value;
            }
        }
    }
}
