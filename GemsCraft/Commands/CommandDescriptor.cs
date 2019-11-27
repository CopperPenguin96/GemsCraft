using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.Players;
using GemsCraft.Players.Ranks;
using GemsCraft.Utils;

namespace GemsCraft.Commands
{
    /// <summary>
    /// Delegate for command handlers/callbacks.
    /// </summary>
    /// <param name="source">Player who called the command.</param>
    /// <param name="cmd">Command arguments</param>
    public delegate void CommandHandler(Player source, Command cmd);

    public sealed class CommandDescriptor: IClassy
    {
        /// <summary>
        /// List of aliases. May be null or empty. Default: null
        /// </summary>
        public string[] Aliases { get; set; }

        /// <summary>
        /// Command category. Must be set before registering.
        /// </summary>
        public CommandCategory Category { get; set; }

        /// <summary>
        /// Whether the command may be used from console. Default: false
        /// </summary>
        public bool IsConsoleSafe { get; set; }

        /// <summary>
        /// Callback function to execute when command is called.
        /// Must be set before registering.
        /// </summary>
        public CommandHandler Handler { get; set; }

        /// <summary>
        /// Full text of the help message. Default: null
        /// </summary>
        public string Help { get; set; }

        /// <summary>
        /// Whether the command is hidden from command list (/cmds).
        /// Default: false
        /// </summary>
        public bool IsHidden { get; set; }

        /// <summary>
        /// Whether the command should be repeated by the "/" shortcut.
        /// Default: false
        /// </summary>
        public bool NotRepeatable { get; set; }

        /// <summary>
        /// Whetherr the command should be usage by frozen players.
        /// Default: false
        /// </summary>
        public bool UsableByFrozenPlayers { get; set; }

        /// <summary>
        /// Whether calls to this command should not be logged.
        /// </summary>
        public bool DisableLogging { get; set; }

        /// <summary>
        /// Primary command name. Must be set before registering.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// List of permissions required to call the command.
        /// May be empty or null.
        /// Default: null
        /// </summary>
        public Permission[] Permissions { get; set; }

        /// <summary>
        /// Whether any permission from the list is enough.
        /// If this is false, ALL permissions are required.
        /// </summary>
        public bool AnyPermission { get; set; }

        /// <summary>
        /// Brief demonstration of command's usage syntax.
        /// Defaults to "/Name"
        /// </summary>
        public string Usage { get; set; }

        /// <summary>
        /// Help sub-sections.
        /// </summary>
        public Dictionary<string, string> HelpSections { get; set; }

        public bool CanBeCalledBy(Rank rank, bool isConsole)
        {
            if (rank == null && !isConsole)
            {
                throw new ArgumentNullException(nameof(rank));
            }
        }
    }
}
