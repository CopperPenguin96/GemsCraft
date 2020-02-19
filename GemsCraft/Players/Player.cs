using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using GemBlocks.Worlds;
using GemsCraft.AppSystem;
using GemsCraft.AppSystem.Logging;
using GemsCraft.AppSystem.Types;
using GemsCraft.ChatSystem;
using GemsCraft.Entities;
using GemsCraft.Entities.Metadata;
using GemsCraft.Network;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace GemsCraft.Players
{
    /// <summary>
    /// Object representing volatile state ("session") of a connected player via Client object.
    /// Also contains information about the known player and their info.
    /// </summary>
    public partial class Player : Living
    {

        #region Properties

        /// <summary>
        /// Used by the server to determine which state it is at when
        /// communicating with the client
        /// </summary>
        public SessionState State;

        /// <summary>
        /// The world that the player is currently on. May be null.
        /// Use .JoinWorld() to make players teleport to another world.
        /// </summary>
        [JsonIgnore]
        public World World { get; private set; }

        /// <summary>
        /// Player's position in the current world.
        /// </summary>
        [JsonIgnore]
        public Position Position { get; set; }

        /// <summary>
        /// Time when the session connected.
        /// </summary>
        public DateTime LoginTime { get; private set; }

        /// <summary>
        /// Last time when the player was active (moving/messaging). UTC.
        /// </summary>
        public DateTime LastActiveTime { get; private set; }

        /// <summary>
        /// Last command called by the player.
        /// </summary>
        [CanBeNull]
       // public Command LastCommand { get; private set; }

        /// <summary>
        /// The unchanging username of the player
        /// </summary>
        [JsonProperty("Username")]
        public string Username { get; internal set; }

        #endregion

        #region Constructors

        internal Player(TcpClient client)
        {
            Client = client;
            Stream = new GameStream(client.GetStream());
        }

        internal Player(TcpClient client, string username, string uuid)
            : this(client)
        {
            Username = username ?? throw new ArgumentNullException(nameof(username));
            UUID = uuid ?? throw new ArgumentNullException(nameof(uuid));
            //spamBackLog = new Queue<DateTime>(Rank.AntiGriefBlocks);
            IP = IPAddress.Loopback;
            //ResetAllBinds();
        }

        internal Player(TcpClient client, string username, string uuid, World world)
            : this(client, username, uuid)
        {
            World = world;
        }

        #endregion

        
        /// <summary>
        /// The Minecraft Client
        /// </summary>
        internal TcpClient Client;

        /// <summary>
        /// Used to identify the player
        /// </summary>
        internal int Eid;

        private byte _slot = 0;

        [JsonProperty("IPBan")]
        public bool IPBanned { get; set; }

        [JsonProperty("TempBan")]
        public long TempBanStart { get; set; } = -1;

        [JsonProperty("TempBanLength")]
        public long TempBanLength { get; set; } = -1;

        [JsonProperty("IP")]
        public IPAddress IP { get; set; }

        [JsonProperty("Rank")]
        public string RankID { get; set; }

        public string LastWorld { get; }
        private bool _regBanned = false;
        public bool IsTempBanned
        {
            get
            {
                if (TempBanStart < 0) return false;

                long timeLeft = (long) (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds;
                return TempBanLength - timeLeft > 0;
            }
        }

        public bool IsBanned
        {
            get => IPBanned || IsTempBanned || _regBanned;
            set => _regBanned = value;
        }

        /// <summary>
        /// Player's current selected slot
        /// </summary>
        public byte Slot
        {
            get => _slot;
            set
            {
                if (value > 8) throw new ArgumentOutOfRangeException(nameof(value));
                _slot = value;
            }
        }

        #region Client-Side Settings

        /// <summary>
        /// eg. en_GB
        /// </summary>
        public string Locale { get; internal set; }

        /// <summary>
        /// Client-side render distance, in chunks
        /// </summary>
        public byte ViewDistance { get; internal set; }

        public ChatMode ChatMode { get; internal set; }

        /// <summary>
        /// "Colors" client-side multiplayer setting
        /// </summary>
        public bool ColorsEnabled { get; internal set; }

        public List<SkinPart> DisplayedSkinParts { get; internal set; }

        #endregion

        #region Metadata

        public EntityMetadata AdditionalHearts = new EntityMetadata(
            11,
            EntityMetadataType.Float,
            0.0
        );

        public EntityMetadata Score = new EntityMetadata(
            12,
            EntityMetadataType.VarInt,
            (VarInt) 0
        );

        public EntityMetadata SkinParts = new EntityMetadata(
            13,
            EntityMetadataType.Byte,
            0
        );

        public EntityMetadata MainHand = new EntityMetadata(
            14,
            EntityMetadataType.Byte,
            1
        );

        public EntityMetadata LeftShoulderEntityData = new EntityMetadata(
            15,
            EntityMetadataType.NBT,
            null
        );

        public EntityMetadata RightShoulderEntityData = new EntityMetadata(
            16,
            EntityMetadataType.NBT,
            null
        );

        #endregion

        /// <summary>
        /// Used during Encryption
        /// </summary>
        protected internal byte[] VerifyToken;

        /// <summary>
        /// Used during Encryption
        /// </summary>
        protected internal byte[] SharedToken;

        /// <summary>
        /// Will be set to true if encryption succeeds
        /// </summary>
        internal bool EncryptionEnabled = false;

        

        /// <summary>
        /// The fixed identifier of the player, used to id them
        /// rather than their username in case the player changes
        /// their username
        /// </summary>
        [JsonProperty("UUID")]
        public string UUID { get; internal set; }

        /// <summary>
        /// Used by the server and client to communicate
        /// </summary>
        internal GameStream Stream;

        

        public void Save()
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            var writer = File.CreateText(Files.PlayerDatabasePath + UUID + ".json");
            writer.Write(json);
            writer.Flush();
            writer.Close();
        }

        public void TrySave()
        {
            try
            {
                Save();
            }
            catch (Exception e)
            {
                Logger.Write(e.ToString());
            }
        }
    }
}
