using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using fNbt;
using GemsCraft.AppSystem.Logging;
using GemsCraft.AppSystem.Types;
using GemsCraft.ChatSystem;
using GemsCraft.Configuration;
using GemsCraft.Entities.Metadata;
using GemsCraft.Players;
using GemsCraft.Utils;
using GemsCraft.Worlds;
using minecraft.level;
using minecraft.world;
using Position = GemsCraft.AppSystem.Types.Position;

namespace GemsCraft.Network.Packets
{
    internal class PlayPackets
    {
        private static int _lastIdentifier = -1;
        public static void JoinGame(Player player, GameStream stream)
        {
            int eid = _lastIdentifier += 1;
            player.Eid = eid;
            string mode = minecraft.level.GameType.CREATIVE.name();
            string dim = Dimension.Overworld.ToString();
            byte max = (byte)Config.Current.MaxPerWorld;
            string levelType = "survival";
            VarInt viewDistance = 2;
            bool reducedInfo = Config.Current.ShowAdvancedDebugInfo;

            Protocol.Send(player, stream, Packet.JoinGame,
                new List<object>
                {
                    eid,
                    mode,
                    dim,
                    max,
                    levelType,
                    viewDistance,
                    reducedInfo
                });
            // TODO: implement brand? (SendPluginMessage)
            SendServerDifficulty(player, stream, Config.Current.Difficulty, true);
        }

        public static void SpawnPosition(Player player, GameStream stream,
            Position pos)
        {
            Protocol.Send(player, stream, Packet.SpawnPosition,
                pos.Get());
        }

        public static void SendChunkData(Player player, GameStream stream,
            Chunk chunk)
        {

        }

        /// <summary>
        /// Mods and plugins can use this to send their data.
        /// Minecraft itself uses several plugin channels. These
        /// internal channels are in the minecraft namespace.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="stream"></param>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        public static void SendPluginMessage(Player player, GameStream stream,
            Identifier channel, byte[] data)
        {
            Protocol.Send(player, stream, Packet.PluginMessage,
                channel.ToString(), data);
        }

        public static void SendServerDifficulty(Player player, GameStream stream,
            Difficulty difficulty, bool locked)
        {
            if ((byte)difficulty > 3) throw new ArgumentOutOfRangeException(nameof(difficulty));
            Protocol.Send(player, stream, Packet.ServerDifficulty,
                (byte)difficulty, locked);
            SendAllPlayerAbilities(player, stream, 0.05F, 0.1F);
        }

        public static void SendAllPlayerAbilities(Player player, GameStream stream,
            float flyingSpeed, float fieldOfViewModifier)
        {
            SendPlayerAbilities(player, stream,
                new[] { Ability.All }, flyingSpeed, fieldOfViewModifier);
        }

        public static void SendPlayerAbilities(Player player, GameStream stream,
            Ability[] abilities, float flyingSpeed, float fieldOfViewModifier)
        {
            if (abilities.Contains(Ability.All))
            {
                abilities = new[]
                {
                    Ability.AllowFlying, Ability.Flying,
                    Ability.InstantBreak, Ability.Invulnerable
                };
            }
            byte abilityList = abilities.Aggregate<Ability, byte>(0, (current, ab) => current.SetBitOn(ab, true));
            Protocol.Send(player, stream, Packet.PlayerAbilities,
                abilityList, flyingSpeed, fieldOfViewModifier);
        }

        public static void ReceivePluginMessage(Player player, GameStream stream)
        {
            string channel = stream.ReadString();
            long lengthLeft = stream.Length - stream.Position;
            byte[] name = stream.ReadByteArray((int)lengthLeft);
            Identifier ident = new Identifier
            {
                Namespace = channel,
                Name = Encoding.UTF8.GetString(name)
            };
            Logger.Write("Received identifier from client: " +
                         ident);
        }

        public static readonly SkinPart[] SkinParts = {
            SkinPart.Cape, SkinPart.Jacket, SkinPart.LeftSleeve,
            SkinPart.RightSleeve, SkinPart.LeftPants, SkinPart.RightPants,
            SkinPart.Hat
        };

        public static void ReceiveClientSettings(Player player, GameStream stream)
        {
            string locale = stream.ReadString();
            byte viewDistance = stream.ReadByte();
            ChatMode chatMode = (ChatMode)(int)stream.ReadVarInt().Value;
            bool colors = stream.ReadBoolean();
            byte skinParts = stream.ReadByte();
            VarInt mainHand = stream.ReadVarInt();

            player.Locale = locale;
            player.ViewDistance = viewDistance;
            player.ChatMode = chatMode;
            player.ColorsEnabled = colors;

            foreach (SkinPart part in SkinParts)
            {
                if (skinParts.IsBitSet(part))
                {
                    player.DisplayedSkinParts.Add(part);
                }
            }

            player.MainHand.Value = mainHand;
            SendPlayerSlot(player, stream, player.Slot);
            // TODO - Declare Recipes
            // TODO - Set Tags
            // TODO - Set Entity Statuses

        }

        /// <summary>
        /// Sent to change the player's slot selection.
        /// </summary>
        /// <param name="slot">The slot which the player has selected 0-8</param>
        public static void SendPlayerSlot(Player player, GameStream stream,
            byte slot)
        {
            if (slot > 8) throw new ArgumentOutOfRangeException(nameof(slot));
            Protocol.Send(player, stream, Packet.HeldItemChange, slot);
        }
    }
}
