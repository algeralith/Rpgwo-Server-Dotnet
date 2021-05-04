using System;
using System.Collections.Generic;
using System.Text;
using Rpgwo_Server.Networking.Packets;

namespace Rpgwo_Server.Networking.Handlers
{
    public class VersionHandler : PacketHandler
    {
        public VersionHandler(PacketTypes packetID, Type packetType) : base(packetID, packetType)
        {
        }

        public override void Handle(NetClient client, Packet packet)
        {
            var versionPacket = (Packets.Version)packet;

            // Are we receiving the packet out of order?
            if (client.NetState != NetState.None)
            {
                // TODO :: Disconnect client? Or just ignore.
            }

            if (versionPacket.Name != Version.ClientName && versionPacket.Number != Version.ClientNumber)
            {
                client.Verified(false);
            }
            else
            {
                client.Verified(true);
            }
        }
    }
}
