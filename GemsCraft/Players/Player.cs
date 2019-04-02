using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem;

namespace GemsCraft.Players
{
    public class Player
    {
        public static Player Console = new Player();
        /// <summary>
        /// The player's minecraft name, unchanged
        /// </summary>
        public string Username { get; set; }

        private bool jsonLoad = false;
        private string identifier;

        /// <summary>
        /// Generic unique identifier of the player. Use this to identify the player
        /// </summary>
        public string UUID
        {
            get => identifier ?? (identifier = Guid.NewGuid().ToString("D"));
            set
            {
                
                if (identifier != null)
                {
                    if (Guid.TryParse(identifier, out Guid result) && jsonLoad)
                    {
                        throw new InvalidOperationException("Player's UUID has already been set.");
                    }
                    Logger.Log(LogType.SystemActivity, $"{Username}'s UUID was invalid. Generating them a new one.", Player.Console);
                    identifier = Guid.NewGuid().ToString("D");
                }
                else
                {
                    if (Guid.TryParse(value, out Guid result))
                    {
                        identifier = value;
                    }
                }
                if (identifier != null && !jsonLoad)
                {
                    jsonLoad = true;
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

        public Player()
        {
            UUID = Guid.NewGuid().ToString("D");
        }

        public void Connection()
        {
            if (Server.PauseConnections) Kick(Player.Console, "Connections paused...", false);
        }

        public void Kick(Player kicker, string reason, bool countKick)
        {
            // TODO - Implement kicking
        }
    }
}
