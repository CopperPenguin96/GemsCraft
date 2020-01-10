// Copyright 2009-2012 Matvei Stefarov <me@matvei.org>
// Modified by apotter96 for GemsCraft

using System;
using System.Diagnostics;
using GemsCraft.Players;
using JetBrains.Annotations;
using GemBlocks.Blocks;
using GemsCraft.Utils;

namespace GemsCraft.Commands
{
    /// <summary>
    /// A text scanner that aids parsing chat commands and their arguments.
    /// Breaks up a message into tokens at spaces. Treats quoted strings as whole tokens.
    /// </summary>
    public sealed class Command : ICloneable
    {
        public CommandDescriptor Descriptor { get; }
        public int Offset { get; set; }
        public readonly string RawMessage;
        public string Name { get; } // lowercase name of the command
        public bool IsConfirmed; // whether this command has been confirmed by the user (with /ok)

        /// <summary>
        /// Creates a copy of an existing command.
        /// </summary>
        public Command([NotNull] Command other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            Offset = other.Offset;
            Descriptor = other.Descriptor;
            RawMessage = other.RawMessage;
            Name = other.Name;
            IsConfirmed = other.IsConfirmed;
        }

        /// <summary>
        /// Creates a command from a raw message.
        /// </summary>
        public Command([NotNull] string rawMessage)
        {
            Offset = 1;
            RawMessage = rawMessage ?? throw new ArgumentNullException(nameof(rawMessage));
            string name = Next();
            if (name == null)
            {
                throw new ArgumentException("Raw message must contain the command name.", nameof(rawMessage));
            }
            Descriptor = CommandManager.GetDescriptor(name, true);
            Name = name.ToLower();
        }


        /// <summary>
        /// Creates a copy of this command.
        /// Use the copy constructor instead of this, if possible.
        /// </summary>
        public object Clone()
        {
            return new Command(this);
        }


        /// <summary> Returns the next command argument.
        /// A single "argument" is either a word that ends with whitespace,
        /// or several words in double quotes (""). </summary>
        /// <returns> Next argument (string), or null if there are no more arguments. </returns>
        [DebuggerStepThrough]
        [CanBeNull]
        public string Next()
        {
            for (; Offset < RawMessage.Length; Offset++)
            {
                int t, j;
                if (RawMessage[Offset] == '"')
                {
                    j = Offset + 1;
                    for (; j < RawMessage.Length && RawMessage[j] != '"'; j++) { }
                    t = Offset;
                    Offset = j;
                    return RawMessage.Substring(t + 1, Offset - t - 1);
                }
                else if (RawMessage[Offset] != ' ')
                {
                    j = Offset;
                    for (; j < RawMessage.Length && RawMessage[j] != ' '; j++) { }
                    t = Offset;
                    Offset = j;
                    return RawMessage.Substring(t, Offset - t);
                }
            }
            return null;
        }


        /// <summary>
        /// Checks whether there is another argument available.
        /// Does not modify the offset.
        /// </summary>
        public bool HasNext
        {
            [DebuggerStepThrough]
            get => Offset < RawMessage.Length;
        }


        /// <summary>
        /// Returns the next command argument, parsed as an integer.
        /// </summary>
        /// <param name="number"> Set to the argument's value if parsing succeeded,
        /// or zero if parsing failed or if there are no more arguments. </param>
        /// <returns> Returns true if parsing succeeded,
        /// and false if parsing failed or if there are no more arguments. </returns>
        [DebuggerStepThrough]
        public bool NextInt(out int number)
        {
            string nextVal = Next();
            if (nextVal == null)
            {
                number = 0;
                return false;
            }
            else
            {
                return int.TryParse(nextVal, out number);
            }
        }


        /// <summary>
        /// Checks whether there there is an int argument available.
        /// Does not modify the offset.
        /// </summary>
        public bool HasInt
        {
            [DebuggerStepThrough]
            get
            {
                if (HasNext)
                {
                    int startOffset = Offset;
                    string nextVal = Next();
                    if (nextVal != null)
                    {
                        if (int.TryParse(nextVal, out int number))
                        {
                            Offset = startOffset;
                            return true;
                        }
                    }
                    Offset = startOffset;
                    return false;
                }
                else
                {
                    return false;
                }
            }
        }


        /// <summary>
        /// Returns the rest of command's text, from current offset to the end of string.
        /// If there is nothing to return (i.e. if string ends at the current offset),
        /// returns empty string.
        /// </summary>
        /// <returns> The rest of the command, or an empty string. </returns>
        [DebuggerStepThrough]
        public string NextAll()
        {
            for (; Offset < RawMessage.Length; Offset++)
            {
                if (RawMessage[Offset] != ' ')
                    return RawMessage.Substring(Offset);
            }
            return "";
        }


        /// <summary>
        /// Counts the number of arguments left in this command.
        /// Does not modify the offset.
        /// </summary>
        public int CountRemaining
        {
            get
            {
                if (HasNext)
                {
                    int startOffset = Offset;
                    int i = 1;
                    while (Next() != null) i++;
                    Offset = startOffset;
                    return i;
                }
                else
                {
                    return 0;
                }
            }
        }


        /// <summary>
        /// Counts the total number of arguments.
        /// Does not modify the offset.
        /// </summary>
        public int Count
        {
            get
            {
                int startOffset = Offset;
                Rewind();
                int i = 1;
                while (Next() != null) i++;
                Offset = startOffset;
                return i;
            }
        }


        /// <summary>
        /// Resets the argument offset.
        /// After calling Rewind, arguments can be read from the beginning again.
        /// </summary>
        public void Rewind()
        {
            Offset = 1;
            Next();
        }


        [DebuggerStepThrough]
        public Block NextBlock([NotNull] Player player)
        {
            if (player == null) throw new ArgumentNullException(nameof(player));
            string blockName = Next();
            Block targetBlock = Block.Undefined;
            if (blockName == null) return targetBlock;
            targetBlock = blockName.GetBlock();
            if (targetBlock == Block.Undefined)
            {
                player.Message("Unrecognized blocktype \"{0}\"", blockName);
            }
            return targetBlock;
        }


        public Block NextBlockWithParam([NotNull] Player player, ref int param)
        {
            if (player == null) throw new ArgumentNullException(nameof(player));
            string jointString = Next();
            if (jointString == null)
            {
                return Block.Undefined;
            }

            Block targetBlock;
            int slashIndex = jointString.IndexOf('/');
            if (slashIndex != -1)
            {
                string blockName = jointString.Substring(0, slashIndex);
                string paramString = jointString.Substring(slashIndex + 1);

                targetBlock = blockName.GetBlock();
                if (targetBlock == Block.Undefined)
                {
                    player.Message("Unrecognized blocktype \"{0}\"", blockName);
                }

                if (int.TryParse(paramString, out var tempParam))
                {
                    param = tempParam;
                }
                else
                {
                    player.Message("Could not parse \"{0}\" as an integer.", paramString);
                }

            }
            else
            {
                bool found = false;
                Block bz = null;
                foreach (Block b in BlockRegistry.Blocks)
                {
                    if (!string.Equals(jointString, b.Name, StringComparison.CurrentCultureIgnoreCase)) continue;
                    found = true;
                    bz = b;
                }

                targetBlock = found ? bz : Block.Undefined;
                if (targetBlock == Block.Undefined)
                {
                    player.Message("Unrecognized blocktype \"{0}\"", jointString);
                }
            }
            return targetBlock;
        }


        public override string ToString()
        {
            return IsConfirmed
                ? $"Command(\"{RawMessage}\",{Offset},confirmed)" 
                : $"Command(\"{RawMessage}\",{Offset})";
        }
    }
}
