using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem.Types;

namespace GemsCraft.Network.Packets
{
    internal enum Packet : byte
    {
        // Handshaking Packets
        Handshake = 0x00, // Serverbound

        // Status Packets
        Response = 0x00, // Clientbound
        Ping = 0x01, // Serverbound
        Pong = 0x01, // Clientbound

        // Login Packets
        LoginStart = 0x00, // Serverbound
        LoginDisconnect = 0x00, // Clientbound
        EncryptionRequest = 0x01, // Clientbound
        EncryptionResponse = 0x01, // Serverbound
        LoginSuccess = 0x02, // Clientbound

        // Play (Clientbound) Packets *This is where the fun begins*
        SpawnObject = 0x00,
        SpawnExperienceOrb = 0x01,
        SpawnGlobalEntity = 0x02,
        SpawnMob = 0x03,
        SpawnPainting = 0x04,
        SpawnPlayer = 0x05,
        Animation = 0x06,
        Statistics = 0x07,
        BlockBreakAnimation = 0x08,
        UpdateBlockEntity = 0x09,
        BlockAction = 0x0A,
        BlockChange = 0x0B,
        BossBar = 0x0C,
        ServerDifficulty = 0x0D,
        ChatMessage = 0x0E,
        MultiBlockChange = 0x0F,
        TabComplete = 0x10,
        DeclareCommands = 0x11,
        ConfirmTransaction = 0x12,
        CloseWindow = 0x13,
        WindowItems = 0x14,
        WindowProperty = 0x15,
        SetSlot = 0x16,
        SetCooldown = 0x17,
        PluginMessage = 0x18,
        NamedSoundEffect = 0x19,
        Disconnect = 0x1A,
        EntityStatus = 0x1B,
        Explosion = 0x1C, // BOOM
        UnloadChunk = 0x1D,
        ChangeGameState = 0x1E,
        OpenHorseWindow = 0x1F, // What does a gay horse say? Haaaay
        KeepAlive = 0x20,
        ChunkData = 0x21,
        Effect = 0x22,
        Particle = 0x23,
        UpdateLight = 0x24,
        JoinGame = 0x25,
        MapData = 0x26,
        TradeList = 0x27,
        EntityRelativeMove = 0x28,
        EntityLookAndRelativeMove = 0x29,
        EntityLook = 0x2A,
        Entity = 0x2B,
        VehicleMove = 0x2C,
        OpenBook = 0x2D,
        OpenWindow = 0x2E,
        OpenSignEditor = 0x2F,
        CraftRecipeResponse = 0x30,
        PlayerAbilities = 0x31,
        CombatEvent = 0x32,
        PlayerInfo = 0x33,
        FacePlayer = 0x34,
        PlayerPositionAndLook = 0x35,
        UnlockRecipes = 0x36,
        DestroyEntities = 0x37,
        RemoveEntityEffect = 0x38,
        ResourcePackSend = 0x39,
        Respawn = 0x3A,
        EntityHeadLook = 0x3B,
        SelectAdvancementTab = 0x3C,
        WorldBorder = 0x3D,
        Camera = 0x3E,
        HeldItemChange = 0x3F,
        UpdateViewPosition = 0x40, // Is this needed?
        UpdateViewDistance = 0x41,
        DisplayScoreboard = 0x42,
        EntityMetadata = 0x43,
        AttachEntity = 0x44,
        EntityVelocity = 0x45,
        EntityEquipment = 0x46,
        SetExperience = 0x47,
        UpdateHealth = 0x48,
        ScoreboardObjective = 0x49,
        SetPassengers = 0x4A,
        Teams = 0x4B,
        UpdateScore = 0x4C,
        SpawnPosition = 0x4D,
        TimeUpdate = 0x4E,
        Title = 0x4F,
        EntitySoundEffect = 0x50,
        SoundEffect = 0x51,
        StopSound = 0x52,
        PlayerListHeaderAndFoter = 0x53,
        NBTQueryResponse = 0x54,
        CollectItem = 0x55,
        EntityTeleport = 0x56,
        Advancements = 0x57,
        EntityProperties = 0x58,
        EntityEffect = 0x59,
        DeclareRecipes = 0x5A,
        Tags = 0x5B,
        AcknolwedgePlayerDigging = 0x5C,

        // Play (Serverbound)
        TeleportConfirm = 0x00,
        QueryBlockNBT = 0x01,
        SetDifficulty = 0x02,
        ChatMessageServerBound = 0x03,
        ClientStatus = 0x04,
        ClientSettings = 0x05,
        TabCompleteServerBound = 0x06,
        ConfirmTransactionServerBound = 0x07,
        ClickWindowButton = 0x08,
        ClickWindow = 0x09,
        CloseWindowServerBound = 0x0A,
        PluginMessageServerBound = 0x0B,
        EditBook = 0x0C,
        QueryEntityNBT = 0x0D,
        UseEntity = 0x0E,
        KeepAliveServerBound = 0x0F,
        LookDifficulty = 0x10,
        PlayerPosition = 0x11,
        PlayerPositonAndLook = 0x12,
        PlayerLook = 0x13,
        Player = 0x14,
        VehicleMoveServerBound = 0x15,
        SteerBoat = 0x16,
        PickItem = 0x17,
        CraftRecipe = 0x18,
        PlayerAbilitiesServerBound = 0x19,
        PlayerDigging = 0x1A,
        EntityAction = 0x1B,
        SteerVehicle = 0x1C,
        RecipeBookData = 0x1D,
        NameItem = 0x1E,
        ResourcePackStatus = 0x1F,
        AdvancementTab = 0x20,
        SelectTrade = 0x21,
        SetBeaconEffect = 0x22,
        HeldItemChangeServerBound = 0x23,
        UpdateCommandBlock = 0x24,
        UpdateCommandBlockMinecart = 0x25,
        CreateInventoryAction = 0x26,
        UpdateJigsawBlock = 0x27,
        UpdateStructureBlock = 0x28,
        UpdateSign = 0x29,
        AnimationServerBound = 0x2A,
        Spectate = 0x2B,
        PlayerBlockPlacement = 0x2C,
        UseItem = 0x2D,
        
        // Other
        Unknown
    }
}
