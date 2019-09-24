using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemsCraft.AppSystem.Exceptions
{
    class ChatBuilderException : Exception
    {
        public ChatBuilderException(string message) : base(message)
        {

        }

        public ChatBuilderException(string message, Exception inner) : base(message, inner)
        {

        }

        public ChatBuilderException(Exception inner) : base("", inner)
        {

        }
    }
}
