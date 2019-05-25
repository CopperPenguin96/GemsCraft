using System.Runtime.InteropServices;
using System.Text;
using GemsCraft.AppSystem;
using GemsCraft.Configuration;
using GemsCraft.Players;
using Newtonsoft.Json;

namespace GemsCraft.Network.Packets
{
    internal interface IPacket
    {
        void Receive(GameStream stream, Player client);

        void Send(GameStream stream, Player client);

        byte GetID();

        VarInt GetLength();

        SessionState GetState();
        
        /* {
             byte packetID = (byte) id.Value;
             switch (state)
             {
                 case SessionState.Handshaking:
                     Send(SessionState.Status, 0x00, stream, );
 
                     break;
             }
         }
         */
        /*public static void Send(SessionState state, VarInt id, GameStream stream, byte[] data)
        {
            switch (state)
            {
                case SessionState.Status: 
                    
                    break;
            }
        }*/


    }

}
