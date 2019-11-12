using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemsCraft.Players
{
    public enum SkinPart: byte
    {
        Cape = 0x01,
        Jacket = 0x02,
        LetSleeve = 0x04,
        RightSleeve = 0x08,
        LeftPants = 0x10,
        RightPants = 0x20,
        Hat = 0x40,
        Unused = 0x80
    }
}
