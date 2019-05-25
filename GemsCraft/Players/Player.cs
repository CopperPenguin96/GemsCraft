using System;
using System.Net.Sockets;
using GemsCraft.AppSystem;
using GemsCraft.Network;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace GemsCraft.Players
{
    public class Player : TcpClient
    {
        public static Player Console;
        public bool IsConsole => this == Console;

        /// <summary>
        /// The player's minecraft name, unchanged.
        /// </summary>
        public string Username { get; internal set; }
        [JsonIgnore] public string ServerId;
        [JsonIgnore] public byte[] VerifyToken;
        [JsonIgnore] public byte[] PublicKey;
        [JsonIgnore] public byte[] SharedKey;
        [JsonIgnore] public AesStream NetworkStream;
        private bool _jsonLoad = false;
        private string _identifier;
        public bool EncryptionEnabled = false;
        [JsonIgnore] public bool LoginCompleted = false;
        /// <summary>
        /// Generic unique identifier of the player. Use this to identify the player
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public string UUID
        {
            get => _identifier ?? (_identifier = Guid.NewGuid().ToString("D"));
            set
            {
                
                if (_identifier != null)
                {
                    if (Guid.TryParse(_identifier, out Guid result) && _jsonLoad)
                    {
                        throw new InvalidOperationException("Player's UUID has already been set.");
                    }
                    Logger.Log(LogType.SystemActivity, $"{Username}'s UUID was invalid. Generating them a new one.", Player.Console);
                    _identifier = Guid.NewGuid().ToString("D");
                }
                else
                {
                    if (Guid.TryParse(value, out Guid result))
                    {
                        _identifier = value;
                    }
                }
                if (_identifier != null && !_jsonLoad)
                {
                    _jsonLoad = true;
                }
            }
        }

        private string displayed;

        /// <summary>
        /// Name displayed to the server, if not changed returns Username
        /// </summary>
        public string DisplayedName
        {
            get => displayed ?? Username;
            set
            {
                if (value != null) displayed = value;
            }
        }
        
        public DateTime LoginTime { get; private set; }

        private Player() { } // Default Constructor made private so forced to use CreateInstance()

        /// <summary>
        /// Either loads or create player based on Minecraft username
        /// </summary>
        /// <param name="username">The Player's Minecraft username</param>
        /// <returns>The player object containing information and details about the player in relation to the server</returns>
        public static Player CreateInstance(string username)
        {
            if (username == null) throw new ArgumentNullException(nameof(username));

            Player player = null;
            foreach (Player p in PlayerDB.AllPlayers())
            {
                if (p.Username == username)
                {
                    player = p;
                }
            }

            // ReSharper disable once ConvertIfStatementToNullCoalescingExpression
            if (player == null)
            {
                player = new Player {UUID = Guid.NewGuid().ToString("D")};
            }

            PlayerDB.LoadPlayerDB();
            return player;
        }

        public bool IsOnline()
        {
            return IsOnline(this);
        }

        public static bool IsOnline(Player player)
        {
            foreach (Player p in Server.OnlinePlayers)
            {
                if (p.UUID == player.UUID)
                {
                    return true;
                }
            }

            return false;
        }

        public int TimesKicked { get; private set; }
        public void Kick(Player kicker, string reason, bool countKick)
        {
            Logger.Log(LogType.Warning,
                $"{this.Username} was kicked by {kicker.Username}: {reason}");
            if (countKick) TimesKicked++;
            Disconnect("Kick");
        }

        public void Disconnect(string reason)
        {
            // TODO impliment disconnect
            Logger.Log(LogType.Normal,
                $"{this.Username} was disconnected: {reason}");
            PlayerDB.SavePlayer(this); // Ensure player data is saved before disconnect
        }

        public void Disconnect()
        {
            Disconnect("Disconnected from the game");
        }

        public void Message(Player player, string message)
        {
            throw new NotImplementedException();
        }

        [StringFormatMethod("message")]
        public void Message(Player player, string message, params object[] formatArgs)
        {
            Message(player, string.Format(message, formatArgs));
        }
    }
}
