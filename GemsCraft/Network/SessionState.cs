using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemsCraft.Network
{
    internal enum SessionState
    {
        Handshaking, // Determining protocol version (Are we on the right MC version?)
        Status, // Sending the Server List Information to the client
        Login // Connecting the user
    }
}
