namespace GemsCraft.ChatSystem
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
