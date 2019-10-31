using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemsCraft.Players
{
    public enum GameMode : byte
    {
        Survival = 0,
        Creative = 1,
        Adventure = 2,
        Spectator = 3,
        Hardcore = 0x8
    }
}
