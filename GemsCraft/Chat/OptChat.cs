using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemsCraft.Chat
{
    public class OptChat
    {
        public bool Enabled = false;

        private ChatBuilder _cht;

        public ChatBuilder Chat
        {
            get => Enabled ? _cht : null;
            set => _cht = value;
        }

        public OptChat(bool enabled, ChatBuilder chat)
        {
            Enabled = enabled;
            Chat = chat;
        }
    }
}
