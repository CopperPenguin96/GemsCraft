﻿namespace GemsCraft.Players
{

    // See comment at the top of Config.cs for a history of changes.

    /// <summary>
    /// Enumeration of permission types/categories.
    /// Every rank definition contains a combination of these.
    /// </summary>
    public enum Permission
    {
        /// <summary>
        /// Ability to chat and to PM players.
        /// Note that players without this permission can still
        /// type in commands, receive PMs, and read chat.
        /// </summary>
        Chat,

        /// <summary>
        /// Ability to place blocks on maps.
        /// This is a baseline permission that can be overridden by
        /// world-specific and zone-specific permissions.
        /// </summary>
        Build,

        /// <summary>
        /// Ability to delete or replace blocks on maps.
        /// This is a baseline permission that can be overridden by
        /// world-specific and zone-specific permissions.
        /// </summary>
        Delete,

        /// <summary>
        /// Ability to place grass blocks.
        /// </summary>
        PlaceGrass,

        /// <summary>
        /// Ability to place water blocks.
        /// </summary>
        PlaceWater,

        /// <summary>
        /// Ability to place lava blocks.
        /// </summary>
        PlaceLava,

        /// <summary>
        /// Ability to build admincrete.
        /// </summary>
        PlaceAdmincrete,

        /// <summary>
        /// Ability to delete or replace admincrete.
        /// </summary>
        DeleteAdmincrete,

        /// <summary>
        /// Ability to set own exit message.
        /// </summary>
        ExitMessage,

        /// <summary>
        /// Ability to set another player's exit message.
        /// </summary>
        SetOtherExitMessage,

        /// <summary>
        /// Ability to view extended information about other players.
        /// </summary>
        ViewOthersInfo,

        /// <summary>
        /// Ability to see any players' IP addresses.
        /// </summary>
        ViewPlayerIPs,

        /// <summary>
        /// Ability to edit the player database directly.
        /// This also adds the ability to promote/demote/ban players by name,
        /// even if they have not visited the server yet. Also allows to
        /// manipulate players' records, and to promote/demote players in batches.
        /// </summary>
        EditPlayerDB,

        /// <summary>
        /// Ability to edit the Server Configuration.
        /// </summary>
        EditConfiguration,

        /// <summary>
        /// Ability to use /Say command.
        /// </summary>
        Say,

        /// <summary>
        /// Ability to read /Staff chat.
        /// </summary>
        ReadStaffChat,

        /// <summary>
        /// Ability to use color codes in chat messages.
        /// </summary>
        UseColorCodes,

        /// <summary>
        /// Ability to move at a faster-than-normal rate (using hacks).
        /// </summary>
        UseSpeedHack,

        /// <summary>
        /// Ability to kick players from the server.
        /// </summary>
        Kick,

        /// <summary>
        /// Ability to ban/unban individual players from the server.
        /// </summary>
        Ban,

        /// <summary>
        /// Ability to ban/unban IP addresses from the server.
        /// </summary>
        BanIP,

        /// <summary>
        /// Ability to ban/unban a player account, his IP, and all other
        /// accounts that used the IP. BanAll/UnbanAll commands can be used
        /// on players who keep evading bans.
        /// </summary>
        BanAll,

        /// <summary>
        /// Ability to promote players to a higher rank.
        /// </summary>
        Promote,

        /// <summary>
        /// Ability to demote players to a lower rank.
        /// </summary>
        Demote,

        /// <summary>
        /// Ability to appear hidden from other players. You can still chat,
        /// build/delete blocks, use all commands, and join worlds while hidden.
        /// Hidden players are completely invisible to other players.
        /// </summary>
        Hide,

        /// <summary>
        /// Ability to use drawing tools (commands capable of affecting
        /// many blocks at once). This permission can be overridden by world-specific
        /// and zone-specific building permissions.
        /// </summary>
        Draw,

        /// <summary>
        /// Ability to use advanced draw commands: sphere, torus, brushes.
        /// </summary>
        DrawAdvanced,

        /// <summary>
        /// Ability to draw trees of any height and shape.
        /// </summary>
        Tree,

        /// <summary>
        /// Ability to copy (or cut) and paste blocks. The total number of
        /// blocks that can be copied or pasted at a time is affected by the draw limit.
        /// </summary>
        CopyAndPaste,

        /// <summary>
        /// Ability to undo actions of other players (UndoArea and UndoPlayer).
        /// </summary>
        UndoOthersActions,

        /// <summary>
        /// Ability to teleport to other players.
        /// </summary>
        Teleport,

        /// <summary>
        /// Ability to bring/summon other players to your location,
        /// or to another player.
        /// </summary>
        Bring,

        /// <summary>
        /// Ability to bring/summon many players at a time.
        /// </summary>
        BringAll,

        /// <summary>
        /// Ability to patrol lower-ranked players.
        /// "Patrolling" means teleporting to other players to check on them,
        /// usually while hidden.
        /// </summary>
        Patrol,

        /// <summary>
        /// Ability to use /Spectate.
        /// </summary>
        Spectate,

        /// <summary>
        /// Ability to freeze/unfreeze players.
        /// Frozen players cannot move or build/delete. (They gotta let it go)
        /// </summary>
        Freeze,

        /// <summary>
        /// Ability to temporarily mute players.
        /// Muted players cannot write chat messages or send PMs,
        /// but they can still type in commands, receive PMs, and read chat.
        /// </summary>
        Mute,

        /// <summary>
        /// Ability to change the spawn point of a world or a player.
        /// </summary>
        SetSpawn,

        /// <summary>
        /// Ability to lock/unlock maps.
        /// "Locking" a map puts it into a protected read-only state.
        /// </summary>
        Lock,

        /// <summary>
        /// Ability to manipulate zones: adding, editing,
        /// renaming, and removing zones.
        /// </summary>
        ManageZones,

        /// <summary>
        /// Ability to manipulate the world list:
        /// adding, renaming, and deleting worlds, loading/saving maps,
        /// changing per-world permissions, and using the map generator.
        /// </summary>
        ManageWorlds,

        /// <summary>
        /// Ability to enable/disable, clear, and configure BlockDB.
        /// </summary>
        ManageBlockDB,

        /// <summary>
        /// Ability to import rank and ban lists from files.
        /// Useful if you are switching from another server software.
        /// </summary>
        Import,

        /// <summary>
        /// Ability to reload the configuration file without restarting.
        /// </summary>
        ReloadConfig,

        /// <summary>
        /// Ability to shut down or restart the server remotely.
        /// Useful for servers that run on dedicated machines.
        /// </summary>
        ShutdownServer,

        /// <summary>
        /// Ability to use portals.
        /// </summary>
        UsePortal,

        /// <summary>
        /// Ability to create or delete a portal.
        /// </summary>
        ManagePortal,

        /// <summary>
        /// Ability to high five someone.
        /// </summary>
        HighFive,

        /// <summary>
        /// Ability to chat in capital letters.
        /// </summary>
        ChatWithCaps,

        /// <summary>
        /// Ability to swear.
        /// </summary>
        Swear,

        /// <summary>
        /// Ability to start VoteKicks in /vote kick
        /// </summary>
        MakeVoteKicks,

        /// <summary>
        /// Ability to start bromode.
        /// </summary>
        BroMode,

        /// <summary>
        /// Ability to impersonate other players (Power Abusable Tool).
        /// </summary>
        Troll,

        /// <summary>
        /// Ability to hide selected ranks.
        /// </summary>
        HideRanks,

        /// <summary>
        /// Ability to read admin chat.
        /// </summary>
        ReadAdminChat,

        /// <summary>
        /// Ability to read from the custom chat channel.
        /// </summary>
        ReadCustomChat,

        /// <summary>
        /// Ability to use realms.
        /// </summary>
        Realm,

        /// <summary>
        /// Ability to possess other players.
        /// </summary>
        Possess,

        /// <summary>
        /// Ability to manually disconnect other players.
        /// </summary>
        Gtfo,

        /// <summary>
        /// Ability to Ragequit from the server.
        /// </summary>
        RageQuit,

        /// <summary>
        /// Ability to tower safely with invisible water towers that cannot harm builds.
        /// </summary>
        Tower,

        /// <summary>
        /// Ability to tempban.
        /// </summary>
        TempBan,

        /// <summary>
        /// Ability to warn other players.
        /// </summary>
        Warn,

        /// <summary>
        /// Ability to slap players to the sky.
        /// </summary>
        Slap,

        /// <summary>
        /// Ability to kill other players.
        /// </summary>
        Kill,

        /// <summary>
        /// Ability to blast players out of the server (kick).
        /// </summary>
        Basscannon,

        /// <summary>
        /// Ability to turn physics on and off.
        /// </summary>
        Physics,

        /// <summary>
        /// Ability to turn turn firework mode on/off.
        /// </summary>
        Fireworks,

        /// <summary>
        /// Ability to use the /Gun for killing, exploding TNT and Portals.
        /// </summary>
        Gun,

        /// <summary>
        /// Ability to use the manage game modes.
        /// </summary>
        Games,

        /// <summary>
        /// Permission to create a server-wide silence, muting all players, and to voice players while the moderation is in affect.
        /// </summary>
        Moderation,

        /// <summary>
        /// Allows a player to become immortal, immune to all damage.
        /// </summary>
        Immortal,

        /// <summary>
        /// Ability to punch.
        /// </summary>
        Punch,

        /// <summary>
        /// Ability to use brofist.
        /// </summary>
        Brofist,

        /// <summary>
        /// Ability to troll a player with STFU.
        /// </summary>
        STFU,

        /// <summary>
        /// Permission to use /muteall.
        /// </summary>
        MuteAll,

        /// <summary>
        /// Permissions to use LeBot.
        /// </summary>
        LeBot,

        /// <summary>
        /// Permission to use GemsCraft Economy
        /// </summary>
        Economy,

        /// <summary>
        /// Permission to manage the economy. (Use /give and /take)
        /// </summary>
        ManageEconomy,

        /// <summary>
        /// Ability to Make votes in /vote
        /// </summary>
        MakeVotes,

        /// <summary>
        /// Permission to toggle WorldChat.
        /// </summary>
        ManageWorldChat,

        /// <summary>
        /// Permission to use /tpa.
        /// </summary>
        TPA,

        /// <summary>
        /// Permission for players to change their own Displayed Name with the /Name command
        /// </summary>
        Name,

        /// <summary>
        /// Permission to manage and edit bots.
        /// </summary>
        Bots,

        /// <summary>
        /// Permission to completely undo a target player's block modifications.
        /// </summary>
        UndoAll
    }
}