using System.Collections.Generic;
using GemBlocks.Blocks;
using GemBlocks.Levels;
using GemBlocks.Levels.Generators;
using GemBlocks.Worlds;
using GemsCraft.AppSystem.Types;
using GemsCraft.Configuration;
using GemsCraft.Players;
using GemsCraft.Worlds;

namespace GemsCraft.Network.Packets
{
    internal class PlayPackets
    {
        private static int _lastIdentifier = -1;
        public static void JoinGame(Player player, GameStream stream)
        {
            int eid = _lastIdentifier += 1;
            player.Eid = eid;
            GameMode mode = GameMode.Survival;
            Dimension dim = Dimension.Overworld;
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

        public static void Gen()
        {
            /*
             * Create the base layers of the generated world.
             * We set the bottom layer of the world to be bedrock
             * and the 20 layers above to be melon
             * blocks.
             */
            DefaultLayers layers = new DefaultLayers();
            layers.SetLayer(0, BlockRegistry.Bedrock);
            layers.SetLayers(1, 20, BlockRegistry.MelonBlock);

            /*
             * Create the internal Minecraft world generator
             * We use a flat generator. We do this to make sure that the
             * whole world will be paved
             * with melons and not just the part we generated.
             */
            IGenerator gen = new FlatGenerator(layers);

            /*
             * Create the level config
             * We set the mode to creative mode and name our world.
             * We also set the spawn point in the middle of our
             * structure
             */
            Level level = new Level("MelonWorld", gen) { GameMode = GameMode.Creative };
            level.SetSpawnPoint(50, 0, 50);
        }
    }
}
