using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.AppSystem.Types;
using GemsCraft.Configuration;
using GemsCraft.Players;
using GemsCraft.Worlds;
using Org.BouncyCastle.Math.EC;

namespace GemsCraft.Network.Packets
{
    internal class PlayPackets
    {
        private static int _lastIdentifier = -1;
        public static void JoinGame(Player player, GameStream stream)
        {
            VarInt id = (VarInt)(byte)Packet.JoinGame;
            int eid = _lastIdentifier += 1;
            player.Eid = eid;
            GameMode mode = GameMode.Survival;
            Dimension dim = Dimension.Overworld;
            byte max = (byte) Config.Current.MaxPerWorld;
            string levelType = LevelType.Default;
            VarInt viewDistance = 2;
            bool reducedInfo = Config.Current.ShowAdvancedDebugInfo;

            // 11 is the size of the regular types of data + 1
            VarInt total = 12 + id.Length + levelType.Length + viewDistance.Length;
            player.Stream.WriteVarInt(total);
            player.Stream.WriteVarInt(id);

            player.Stream.WriteInt32(eid);
            player.Stream.WriteByte((byte) mode);
            player.Stream.WriteInt32((int) dim);
            player.Stream.WriteByte(max);
            player.Stream.WriteString(levelType);
            player.Stream.WriteVarInt(viewDistance);
            player.Stream.WriteBoolean(reducedInfo);
        }
    }
}
