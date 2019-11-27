using System;

namespace GemsCraft.AppSystem.Exceptions
{
    public sealed class ChatBuilderException : Exception
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
