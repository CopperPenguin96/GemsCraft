// Copyright 2009-2012 Matvei Stefarov <me@matvei.org>
// Modified by apotter96 for GemsCraft
using System;
using System.Net;
using GemsCraft.Configuration;
using JetBrains.Annotations;

namespace GemsCraft.Players
{
    public sealed class PlayerOpException : Exception
    {
        public PlayerOpException([NotNull] Player player,
                                  PlayerOpExceptionCode errorCode,
                                  [NotNull] string message, [NotNull] string messageColored)
            : base(message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));
            Player = player ?? throw new ArgumentNullException(nameof(player));
            ErrorCode = errorCode;
            MessageColored = messageColored ?? throw new ArgumentNullException(nameof(messageColored));
        }

        public Player Player { get; private set; }
        public PlayerOpExceptionCode ErrorCode { get; private set; }
        public string MessageColored { get; private set; }

        /// <summary>
        /// Throws a PlayerOpException if reason is required but missing.
        /// </summary>
        internal static void CheckBanReason([CanBeNull] string reason, [NotNull] Player player, bool unban)
        {
            if (player == null) throw new ArgumentNullException(nameof(player));
            if (!Config.Advanced.RequireBanReason || !string.IsNullOrEmpty(reason)) return;
            var msg = unban ? "Please specify an unban reason." : "Please specify an ban reason.";
            string colorMsg = "&S" + msg;
            throw new PlayerOpException(player, PlayerOpExceptionCode.ReasonRequired, msg, colorMsg);
        }
        
        /// <summary>
        /// Throws a PlayerOpException if reason is required but missing.
        /// </summary>
        /// <param name="reason"></param>
        /// <param name="player"></param>
        /// <param name="promoting"></param>
        internal static void CheckRankChangeReason([CanBeNull] string reason, [NotNull] Player player, bool promoting)
        {
            if (player == null) throw new ArgumentNullException(nameof(player));
            if (!Config.Advanced.RequireRankChangeReason || !string.IsNullOrEmpty(reason)) return;
            var msg = promoting ? "Please specify a promotion reason." : "Please specify a demotion reason.";
            string colorMsg = "&S" + msg;
            throw new PlayerOpException(player, PlayerOpExceptionCode.ReasonRequired, msg, colorMsg);
        }
        
        /// <summary>
        /// Throws a PlayerOpException if reason is required but missing.
        /// </summary>
        internal static void CheckKickReason([CanBeNull] string reason, [NotNull] Player player)
        {
            if (player == null) throw new ArgumentNullException(nameof(player));
            if (!Config.Advanced.RequireKickReason || !string.IsNullOrEmpty(reason)) return;
            const string msg = "Please specify a kick reason.";
            const string colorMsg = "&S" + msg;
            throw new PlayerOpException(player, PlayerOpExceptionCode.ReasonRequired, msg, colorMsg);
        }


        [ContractAnnotation("=> halt")]
        internal static void ThrowCannotTargetSelf([NotNull] Player player, [NotNull] string action)
        {
            if (player == null) throw new ArgumentNullException(nameof(player));
            if (action == null) throw new ArgumentNullException(nameof(action));
            string msg = $"You cannot {action} yourself.";
            string colorMsg = $"&SYou cannot {action} yourself.";
            throw new PlayerOpException(player, PlayerOpExceptionCode.CannotTargetSelf, msg, colorMsg);
        }


        [ContractAnnotation("=> halt")]
        internal static void ThrowPermissionMissing([NotNull] Player player, 
                                              [NotNull] string action, [NotNull] params Permission[] permissions)
        {
            if (player == null) throw new ArgumentNullException(nameof(player));
            if (action == null) throw new ArgumentNullException(nameof(action));
            if (permissions == null) throw new ArgumentNullException(nameof(permissions));
            Rank minRank = RankManager.GetMinRankWithAllPermissions(permissions);
            string msg, colorMsg;
            if (minRank != null)
            {
                msg = $"You need to be ranked {minRank.Name}+ to {action}.";
                colorMsg = $"&SYou need to be ranked {minRank.ClassyName}&S+ to {action}.";
            }
            else
            {
                msg = $"No one is allowed to {action} on this server.";
                colorMsg = $"&SNo one is allowed to {action} on this server.";
            }

            throw new PlayerOpException(player, PlayerOpExceptionCode.PermissionMissing, msg, colorMsg);
        }


        [ContractAnnotation("=> halt")]
        internal static void ThrowPermissionLimitIP([NotNull] Player player, [NotNull] Player playerWhoPlayerWhoCantBan, [NotNull] IPAddress targetAddress)
        {
            if (player == null) throw new ArgumentNullException(nameof(player));
            if (playerWhoPlayerWhoCantBan == null) throw new ArgumentNullException("infoWhomPlayerCantBan");
            if (targetAddress == null) throw new ArgumentNullException(nameof(targetAddress));
            string msg, colorMsg;
            if (player.Can(Permission.ViewPlayerIPs))
            {
                msg =
                    $"IP {targetAddress} is used by player {playerWhoPlayerWhoCantBan}, ranked {playerWhoPlayerWhoCantBan.Rank.Name}. You may only ban players ranked {player.Info.Rank.GetLimit(Permission.Ban).Name} and below.";
                colorMsg = String.Format("&SIP {0} is used by player {1}&S, ranked {2}&S. You may only ban players ranked {3}&S and below.",
                                          targetAddress, infoWhomPlayerCantBan.ClassyName, infoWhomPlayerCantBan.Rank.ClassyName,
                                          player.Info.Rank.GetLimit(Permission.Ban).ClassyName);
            }
            else
            {
                msg = String.Format("Given IP is used by player {0}, ranked {1}. You may only ban players ranked {2} and below.",
                                     infoWhomPlayerCantBan.Name, infoWhomPlayerCantBan.Rank.Name,
                                     player.Info.Rank.GetLimit(Permission.Ban).Name);
                colorMsg = String.Format("&SGiven IP is used by player {0}&S, ranked {1}&S. You may only ban players ranked {2}&S and below.",
                                          infoWhomPlayerCantBan.ClassyName, infoWhomPlayerCantBan.Rank.ClassyName,
                                          player.Info.Rank.GetLimit(Permission.Ban).ClassyName);
            }
            throw new PlayerOpException(player, infoWhomPlayerCantBan, PlayerOpExceptionCode.PermissionLimitTooLow,
                                         msg, colorMsg);
        }


        [ContractAnnotation("=> halt")]
        internal static void ThrowPermissionLimit([NotNull] Player player, [NotNull] PlayerInfo targetInfo, [NotNull] string action, Permission permission)
        {
            if (player == null) throw new ArgumentNullException("player");
            if (targetInfo == null) throw new ArgumentNullException("targetInfo");
            if (action == null) throw new ArgumentNullException("action");
            string msg = String.Format("Cannot {0} {1} (ranked {2}): you may only {0} players ranked {3} and below.",
                                        action, targetInfo.Name, targetInfo.Rank.Name,
                                        player.Info.Rank.GetLimit(permission).Name);
            string colorMsg = String.Format("&SCannot {0} {1}&S (ranked {2}&S): you may only {0} players ranked {3}&S and below.",
                                             action, targetInfo.ClassyName, targetInfo.Rank.ClassyName,
                                             player.Info.Rank.GetLimit(permission).ClassyName);
            throw new PlayerOpException(player, targetInfo, PlayerOpExceptionCode.PermissionLimitTooLow,
                                         msg, colorMsg);
        }


        [TerminatesProgram]
        internal static void ThrowPlayerAndIPNotBanned([NotNull] Player player, [NotNull] PlayerInfo targetInfo, [NotNull] IPAddress address)
        {
            if (player == null) throw new ArgumentNullException("player");
            if (targetInfo == null) throw new ArgumentNullException("targetInfo");
            if (address == null) throw new ArgumentNullException("address");
            string msg, colorMsg;
            if (player.Can(Permission.ViewPlayerIPs))
            {
                msg = String.Format("Player {0} and their IP ({1}) are not currently banned.",
                                     targetInfo.Name, address);
                colorMsg = String.Format("&SPlayer {0}&S and their IP ({1}) are not currently banned.",
                                     targetInfo.ClassyName, address);
            }
            else
            {
                msg = String.Format("Player {0} and their IP are not currently banned.",
                                     targetInfo.Name);
                colorMsg = String.Format("&SPlayer {0}&S and their IP are not currently banned.",
                                     targetInfo.ClassyName);
            }
            throw new PlayerOpException(player, targetInfo, PlayerOpExceptionCode.NoActionNeeded, msg, colorMsg);
        }


        [TerminatesProgram]
        internal static void ThrowNoOneToBan([NotNull] Player player, [CanBeNull] PlayerInfo targetInfo, [NotNull] IPAddress address)
        {
            if (player == null) throw new ArgumentNullException("player");
            if (address == null) throw new ArgumentNullException("address");
            string msg, colorMsg;
            if (targetInfo == null)
            {
                if (player.Can(Permission.ViewPlayerIPs))
                {
                    msg = String.Format("Given IP ({0}) and all players who use it are already banned.",
                                         address);
                }
                else
                {
                    msg = "Given IP and all players who use it are already banned.";
                }
                colorMsg = "&S" + msg;
            }
            else
            {
                if (player.Can(Permission.ViewPlayerIPs))
                {
                    msg = String.Format("Player {0}, their IP ({1}), and all players who use this IP are already banned.",
                                         targetInfo.Name, address);
                    colorMsg = String.Format("&SPlayer {0}&S, their IP ({1}), and all players who use this IP are already banned.",
                                              targetInfo.ClassyName, address);
                }
                else
                {
                    msg = String.Format("Player {0}, their IP, and all players who use this IP are already banned.",
                                         targetInfo.Name);
                    colorMsg = String.Format("&SPlayer {0}&S, their IP, and all players who use this IP are already banned.",
                                              targetInfo.ClassyName);
                }
            }
            throw new PlayerOpException(player, targetInfo, PlayerOpExceptionCode.NoActionNeeded, msg, colorMsg);
        }


        [TerminatesProgram]
        internal static void ThrowNoOneToUnban([NotNull] Player player, [CanBeNull] PlayerInfo targetInfo, [NotNull] IPAddress address)
        {
            if (player == null) throw new ArgumentNullException("player");
            if (address == null) throw new ArgumentNullException("address");
            string msg;
            if (player.Can(Permission.ViewPlayerIPs))
            {
                msg = String.Format("None of the players who use given IP ({0}) are banned.",
                                     address);
            }
            else
            {
                msg = "None of the players who use given IP are banned.";
            }
            string colorMsg = "&S" + msg;
            throw new PlayerOpException(player, targetInfo, PlayerOpExceptionCode.NoActionNeeded, msg, colorMsg);
        }


        [TerminatesProgram]
        internal static void ThrowPlayerAlreadyBanned([NotNull] Player player, [NotNull] PlayerInfo target, [NotNull] string action)
        {
            if (player == null) throw new ArgumentNullException("player");
            if (target == null) throw new ArgumentNullException("target");
            if (action == null) throw new ArgumentNullException("action");
            string msg = String.Format("Player {0} is already {1}.", target.Name, action);
            string msgColored = String.Format("&SPlayer {0}&S is already {1}.", target.ClassyName, action);
            throw new PlayerOpException(player, target, PlayerOpExceptionCode.NoActionNeeded, msg, msgColored);
        }


        [TerminatesProgram]
        internal static void ThrowPlayerNotBanned([NotNull] Player player, [NotNull] PlayerInfo target, [NotNull] string action)
        {
            if (player == null) throw new ArgumentNullException("player");
            if (target == null) throw new ArgumentNullException("target");
            if (action == null) throw new ArgumentNullException("action");
            string msg = String.Format("Player {0} is not currently {1}.", target.Name, action);
            string msgColored = String.Format("&SPlayer {0}&S is not currently {1}.", target.ClassyName, action);
            throw new PlayerOpException(player, target, PlayerOpExceptionCode.NoActionNeeded, msg, msgColored);
        }


        [TerminatesProgram]
        internal static void ThrowCancelled([NotNull] Player player, [NotNull] PlayerInfo target)
        {
            if (player == null) throw new ArgumentNullException("player");
            if (target == null) throw new ArgumentNullException("target");
            const string msg = "Cancelled by plugin.";
            const string colorMsg = "&S" + msg;
            throw new PlayerOpException(player, target, PlayerOpExceptionCode.Cancelled, msg, colorMsg);
        }


        [TerminatesProgram]
        internal static void ThrowNoWorld([NotNull] Player player)
        {
            if (player == null) throw new ArgumentNullException("player");
            const string msg = "Player must be in a world to do this.";
            const string colorMsg = "&S" + msg;
            throw new PlayerOpException(player, null, PlayerOpExceptionCode.MustBeInAWorld, msg, colorMsg);
        }


        [TerminatesProgram]
        internal static void ThrowInvalidIP([NotNull] Player player, [CanBeNull] PlayerInfo target, [NotNull] IPAddress ip)
        {
            if (player == null) throw new ArgumentNullException("player");
            if (ip == null) throw new ArgumentNullException("ip");
            string msg, msgColored;
            if (target == null)
            {
                msg = String.Format("Cannot IP-ban {0}: invalid IP.", ip);
                msgColored = String.Format("&SCannot IP-ban {0}: invalid IP.", ip);
            }
            else
            {
                msg = String.Format("Cannot IP-ban {0}: invalid IP ({1}).", target.ClassyName, ip);
                msgColored = String.Format("&SCannot IP-ban {0}&S: invalid IP ({1}).", target.ClassyName, ip);
            }
            throw new PlayerOpException(player, target, PlayerOpExceptionCode.NoActionNeeded, msg, msgColored);
        }
    }


    public enum PlayerOpExceptionCode
    {
        /// <summary> Other/unknown/unexpected error. </summary>
        Other,

        /// <summary> Player cannot execute this operation on himself/herself. </summary>
        CannotTargetSelf,

        /// <summary> Operation is not needed (e.g. target is already in the desired state). </summary>
        NoActionNeeded,

        /// <summary> Server configuration requires a reason to be given to complete this operation. </summary>
        ReasonRequired,

        /// <summary> A permission needed to execute this operation is missing. </summary>
        PermissionMissing,

        /// <summary> All needed permissions are present, but rank limit is not high enough. </summary>
        PermissionLimitTooLow,

        /// <summary> Target cannot be affected by this operation. </summary>
        TargetIsExempt,

        /// <summary> Player must have a world to execute this operation. </summary>
        MustBeInAWorld,

        /// <summary> Operation was cancelled by an event callback (e.g. by a mod or a plugin). </summary>
        Cancelled,

        /// <summary> Cannot ban IPAddress.Any or IPAddress.All. </summary>
        InvalidIP
    }
}