using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using GemsCraft.AppSystem;
using GemsCraft.AppSystem.Logging;
using GemsCraft.AppSystem.Types;
using GemsCraft.Chat;
using GemsCraft.Entities;
using GemsCraft.Entities.Metadata;
using GemsCraft.Network;
using Newtonsoft.Json;

namespace GemsCraft.Players
{
    public class Player : Living
    {
        /// <summary>
        /// The Minecraft Client
        /// </summary>
        internal TcpClient Client;
        /// <summary>
        /// Used to identify the player
        /// </summary>
        internal int Eid;
        /// <summary>
        /// Used by the server to determine which state it is at when
        /// communicating with the client
        /// </summary>
        public SessionState State;

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
        /// The unchanging username of the player
        /// </summary>
        [JsonProperty("Username")]
        public string Username { get; internal set; }

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

        public Player(TcpClient client)
        {
            Client = client;
            Stream = new GameStream(client.GetStream());
        }

        public Player(string username, string uuid)
        {
            Username = username;
            UUID = uuid;
        }

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
