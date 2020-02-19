using System;
using GemBlocks.Blocks;
using GemBlocks.Worlds;
using GemsCraft.AppSystem.Events.Players;
using GemsCraft.AppSystem.Types;
using GemsCraft.Utils;
using GemsCraft.Worlds;
using java.util;
using JetBrains.Annotations;

namespace GemsCraft.Players
{
    partial class Player
    {
        /// <summary>
        /// Occurs when a player is connecting (cancellable).
        /// Player name is verified and bans are checked before this event is raised.
        /// </summary>
        public static event EventHandler<PlayerConnectingEventArgs> Connecting;


        /// <summary>
        /// Occurs when a player has connected, but before the player has joined any world.
        /// Allows changing the player's starting world.
        /// </summary>
        public static event EventHandler<PlayerConnectedEventArgs> Connected;


        /// <summary>
        /// Occurs after a player has connected and joined the starting world.
        /// </summary>
        public static event EventHandler<PlayerEventArgs> Ready;


        /// <summary>
        /// Occurs when player is about to move (cancellable).
        /// </summary>
        public static event EventHandler<PlayerMovingEventArgs> Moving;


        /// <summary>
        /// Occurs when player has moved.
        /// </summary>
        public static event EventHandler<PlayerMovedEventArgs> Moved;


        /// <summary>
        /// Occurs when player clicked a block (cancellable).
        /// Note that a click will not necessarily result in a block being placed or deleted.
        /// </summary>
        public static event EventHandler<PlayerClickingEventArgs> Clicking;


        /// <summary>
        /// Occurs after a player has clicked a block.
        /// Note that a click will not necessarily result in a block being placed or deleted.
        /// </summary>
        public static event EventHandler<PlayerClickedEventArgs> Clicked;


        /// <summary>
        /// Occurs when a player is about to place a block.
        /// Permission checks are done before calling this event, and their result may be overridden.
        /// </summary>
        public static event EventHandler<PlayerPlacingBlockEventArgs> PlacingBlock;


        /// <summary>
        /// Occurs when a player has placed a block.
        /// This event does not occur if the block placement was disallowed.
        /// </summary>
        public static event EventHandler<PlayerPlacedBlockEventArgs> PlacedBlock;


        /// <summary>
        /// Occurs before a player is kicked (cancellable). 
        /// Kick may be caused by /Kick, /Ban, /BanIP, or /BanAll commands, or by idling.
        /// Callbacks may override whether the kick will be announced or recorded in PlayerDB.
        /// </summary>
        public static event EventHandler<PlayerBeingKickedEventArgs> BeingKicked;


        /// <summary>
        /// Occurs after a player has been kicked. Specifically, it happens after
        /// kick has been announced and recorded to PlayerDB (if applicable), just before the
        /// target player disconnects.
        /// Kick may be caused by /Kick, /Ban, /BanIP, or /BanAll commands, or by idling.
        /// </summary>
        public static event EventHandler<PlayerKickedEventArgs> Kicked;


        /// <summary>
        /// Happens after a player has hidden or unhidden.
        /// </summary>
        public static event EventHandler<PlayerEventArgs> HideChanged;


        /// <summary>
        /// Occurs when a player disconnects.
        /// </summary>
        public static event EventHandler<PlayerDisconnectedEventArgs> Disconnected;


        /// <summary>
        /// Occurs when a player intends to join a world (cancellable).
        /// </summary>
        public static event EventHandler<PlayerJoiningWorldEventArgs> JoiningWorld;


        /// <summary>
        /// Occurs after a player has joined a world.
        /// </summary>
        public static event EventHandler<PlayerJoinedWorldEventArgs> JoinedWorld;


        private static bool RaisePlayerConnectingEvent([NotNull] Player player)
        {
            if (player == null) throw new ArgumentNullException(nameof(player));
            var h = Connecting;
            if (h == null) return false;
            var e = new PlayerConnectingEventArgs(player);
            h(null, e);
            return e.Cancel;
        }


        private static World RaisePlayerConnectedEvent([NotNull] Player player, World world)
        {
            if (player == null) throw new ArgumentNullException(nameof(player));
            var h = Connected;
            if (h == null) return world;
            var e = new PlayerConnectedEventArgs(player, world);
            h(null, e);
            return e.StartingWorld;
        }


        private static void RaisePlayerReadyEvent([NotNull] Player player)
        {
            if (player == null) throw new ArgumentNullException(nameof(player));
            var h = Ready;
            h?.Invoke(null, new PlayerEventArgs(player));
        }


        private static bool RaisePlayerMovingEvent([NotNull] Player player, Position newPos)
        {
            if (player == null) throw new ArgumentNullException(nameof(player));
            var h = Moving;
            if (h == null) return false;
            var e = new PlayerMovingEventArgs(player, newPos);
            h(null, e);
            return e.Cancel;
        }


        public static void RaisePlayerMovedEvent([NotNull] Player player, Position oldPos)
        {
            if (player == null) throw new ArgumentNullException(nameof(player));
            var h = Moved;
            h?.Invoke(null, new PlayerMovedEventArgs(player, oldPos));
        }


        public static bool RaisePlayerClickingEvent([NotNull] PlayerClickingEventArgs e)
        {
            if (e == null) throw new ArgumentNullException(nameof(e));
            var h = Clicking;
            if (h == null) return false;
            h(null, e);
            return e.Cancel;
        }


        public static void RaisePlayerClickedEvent(Player player, Vector3I coords,
                                              ClickAction action, Block block)
        {
            var handler = Clicked;
            handler?.Invoke(null, new PlayerClickedEventArgs(player, coords, action, block));
        }


        public static void RaisePlayerPlacedBlockEvent(Player player, Map map, Vector3I coords,
                                                          Block oldBlock, Block newBlock, BlockChangeContext context)
        {
            var handler = PlacedBlock;
            handler?.Invoke(null, new PlayerPlacedBlockEventArgs(player, map, coords, oldBlock, newBlock, context));
        }


        private static void RaisePlayerBeingKickedEvent([NotNull] PlayerBeingKickedEventArgs e)
        {
            if (e == null) throw new ArgumentNullException(nameof(e));
            var h = BeingKicked;
            h?.Invoke(null, e);
        }


        private static void RaisePlayerKickedEvent([NotNull] PlayerKickedEventArgs e)
        {
            if (e == null) throw new ArgumentNullException(nameof(e));
            var h = Kicked;
            h?.Invoke(null, e);
        }


        internal static void RaisePlayerHideChangedEvent([NotNull] Player player)
        {
            if (player == null) throw new ArgumentNullException(nameof(player));
            var h = HideChanged;
            h?.Invoke(null, new PlayerEventArgs(player));
        }


        private static void RaisePlayerDisconnectedEvent([NotNull] Player player, LeaveReason leaveReason)
        {
            if (player == null) throw new ArgumentNullException(nameof(player));
            var h = Disconnected;
            h?.Invoke(null, new PlayerDisconnectedEventArgs(player, leaveReason, false));
        }


        private static bool RaisePlayerJoiningWorldEvent([NotNull] Player player, [NotNull] World newWorld, WorldChangeReason reason,
                                                  string textLine1, string textLine2)
        {
            if (player == null) throw new ArgumentNullException(nameof(player));
            if (newWorld == null) throw new ArgumentNullException(nameof(newWorld));
            var h = JoiningWorld;
            if (h == null) return false;
            var e = new PlayerJoiningWorldEventArgs(player, player.World, newWorld, reason, textLine1, textLine2);
            h(null, e);
            return e.Cancel;
        }


        private static void RaisePlayerJoinedWorldEvent(Player player, World oldWorld, WorldChangeReason reason)
        {
            var h = JoinedWorld;
            h?.Invoke(null, new PlayerJoinedWorldEventArgs(player, oldWorld, player.World, reason));
        }
    }
}
