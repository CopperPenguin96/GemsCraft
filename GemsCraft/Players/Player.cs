using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem;
using GemsCraft.Configuration;
using GemsCraft.Utils;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace GemsCraft.Players
{
    public class Player
    {
        public static Player Console;
        public bool IsConsole => this == Console;
        public TcpClient Client;

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
        
        public DateTime LoginTime { get; private set; }
        /// <summary>
        /// Used to create Console like players
        /// </summary>
        public Player(string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            UUID = Guid.NewGuid().ToString("D");
        }

        
        public void Kick(Player kicker, string reason, bool countKick)
        {
            // TODO - Implement kicking
        }


    }
}
