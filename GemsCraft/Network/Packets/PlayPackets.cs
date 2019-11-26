using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using fNbt;
using GemsCraft.AppSystem.Logging;
using GemsCraft.AppSystem.Types;
using GemsCraft.Chat;
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
            string mode = GameType.CREATIVE.name();
            string dim = Dimension.Overworld.ToString();
            byte max = (byte) Config.Current.MaxPerWorld;
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
        /// Sent by the server when a vehicle or other object is spawned
        /// </summary>
        /// <param name="player">Active player</param>
        /// <param name="stream"></param>
        /// <param name="id">EID of the object</param>
        /// <param name="uuid"></param>
        /// <param name="type">The type of the object (same as in Spawn Mob)</param>
        /// <param name="loc">Location reletive to the world</param>
        /// <param name="data">Meaning dependent on the value of the Type
        /// field, see Object Data for details</param>
        /// <param name="vel">Same units as Entity Velocity,
        /// Always sent, but only used when Data is greater than 0
        /// (Except for soem entities which always Ignore It)</param>
        public static void SpawnObject(Player player, GameStream stream, 
            VarInt id, string uuid, VarInt type,
            Location loc, int data, Velocity vel)
        {
            Protocol.Send(player, stream, Packet.SpawnObject,
                new List<object>
                {
                    uuid,
                    type,
                    loc.X,
                    loc.Y,
                    loc.Z,
                    loc.Pitch,
                    loc.Yaw,
                    data,
                    vel.X,
                    vel.Y,
                    vel.Z
                });
        }

        /// <summary>
        /// Spawn one or more experience orbs
        /// </summary>
        /// <param name="entID">Entity ID</param>
        /// <param name="loc">The location of the entity</param>
        /// <param name="count">The amount of experience this orb will reward once collected</param>
        public static void SpawnExperienceOrb(Player player, GameStream stream,
            VarInt entID, Location loc, short count)
        {
            Protocol.Send(player, stream, Packet.SpawnExperienceOrb,
                new List<object>
                {
                    entID,
                    loc.X,
                    loc.Y,
                    loc.Z,
                    count
                });
        }

        /// <summary>
        /// With this packet, the server notifies the client of thunderbolts
        /// striking within a 512 block radius around the player.
        /// The coordinates specify where exactly the thunderbolt strikes.
        /// BOOM
        /// </summary>
        /// <param name="entID">The EID of the thunderbolt</param>
        /// <param name="enu">The global entity type, currently
        /// always 1 for thunderbolt</param>
        public static void SpawnGlobalEntity(Player player, GameStream stream,
            VarInt entID, byte enu, Location location)
        {
            Protocol.Send(player, stream, Packet.SpawnGlobalEntity,
                entID, enu,
                location.X, location.Y, location.Z
                );
        }

        public static void SpawnMob(Player player, GameStream stream,
            VarInt eID, string uuid, Mob mob, Location loc, Velocity vel,
            EntityMetadata metadata)
        {
            Protocol.Send(player, stream, Packet.SpawnMob,
                eID, uuid, (VarInt) (int)mob, 
                loc.X, loc.Y, loc.Z, loc.Yaw, loc.Pitch, loc.HeadPitch,
                vel.X, vel.Y, vel.Z, metadata);
        }

        public static void SpawnPainting(Player player, GameStream stream,
            VarInt eID, string uuid, VarInt motive, Location loc, byte dir)
        {
            Protocol.Send(player, stream,
                Packet.SpawnPainting, 
                eID, uuid, motive, loc, dir);
        }

        /// <summary>
        /// Sent when a player comes into visible range, not when a player joins!
        /// </summary>
        public static void SpawnPlayer(Player player, GameStream stream,
            VarInt eID, string uuid, Location loc, EntityMetadata metadata)
        {
            Protocol.Send(player, stream, Packet.SpawnPlayer,
                eID, uuid, 
                loc.X, loc.Y, loc.Z,
                loc.Yaw, loc.Pitch, metadata);
        }

        public static void Animation(Player player, GameStream stream,
            VarInt eID, Animation animation)
        {
            Protocol.Send(player, stream,
                Packet.Animation, eID, (byte) animation);
        }

        public static void Statistics(Player player, GameStream stream,
            VarInt count, VarInt[] statistic, VarInt value)
        {
            throw new NotImplementedException();
        }

        public static void BlockBreakAnimation(Player player, GameStream stream,
            VarInt eID, Location loc, byte destroyStage)
        {
            if (destroyStage > 9) throw new ArgumentOutOfRangeException(nameof(destroyStage));
            Protocol.Send(player, stream, Packet.BlockBreakAnimation,
                eID, loc, destroyStage);
        }

        public static void UpdateBlockEntity(Player player, GameStream stream,
            Location loc, byte action, NbtTag tag)
        {
            throw new NotImplementedException();
        }

        public static void BlockAction(Player player, GameStream stream,
            Position pos, byte actionID, byte actionParam, VarInt type)
        {
            Protocol.Send(player, stream, Packet.BlockAction,
                pos.Get(), actionID, actionParam, type);
        }

        public static void BlockChange(Player player, GameStream stream,
            Position pos, VarInt id)
        {
            Protocol.Send(player, stream, Packet.BlockChange,
                pos.Get(), id);
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
            if ((byte) difficulty > 3) throw new ArgumentOutOfRangeException(nameof(difficulty));
            Protocol.Send(player, stream, Packet.ServerDifficulty,
                (byte) difficulty, locked);
            SendAllPlayerAbilities(player, stream,0.05F, 0.1F);
        }

        public static void SendAllPlayerAbilities(Player player, GameStream stream,
            float flyingSpeed, float fieldOfViewModifier)
        {
            SendPlayerAbilities(player, stream,
                new[] {Ability.All}, flyingSpeed, fieldOfViewModifier);
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
            byte[] name = stream.ReadByteArray((int) lengthLeft);
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
            ChatMode chatMode = (ChatMode) (int) stream.ReadVarInt().Value;
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
        }
    }
}
