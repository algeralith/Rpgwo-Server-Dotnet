using System;
using System.Collections.Generic;
using System.Text;
using Rpgwo_Server.Networking.Packets;

namespace Rpgwo_Server.Networking.Handlers
{
    public class Info2Handler : PacketHandler
    {
        public Info2Handler(PacketTypes packetID, Type packetType) : base(packetID, packetType)
        {

        }

        // This packet is only ever received once for the life of the socket, and only when the user first logins.
        // Otherwise, it is never sent again, even if the user logouts and logins back in again.
        public override void Handle(NetClient client, Packet packet)
        {
            var info2Packet = (Info2)packet;

            byte[] tmp = new byte[30];
            for (int i = 0; i < 30; i++)
            {
                    tmp[i] = (byte)(info2Packet.Data1[i] ^ info2Packet.Data1[i + 30]);
            }

            String hID = Encoding.UTF8.GetString(tmp); // TODO :: 

            byte[] tmp2 = new byte[30];
            for (int i = 0; i < 30; i++)
            {
                tmp2[i] = (byte)(info2Packet.Data2[i] ^ info2Packet.Data2[i + 30]);
            }

            String mID = Encoding.UTF8.GetString(tmp2); // Not sure if this is actually mac. Server just labels it as M.

            // TODO :: Whatever I need to do to keep track of this.
        }
    }
}
