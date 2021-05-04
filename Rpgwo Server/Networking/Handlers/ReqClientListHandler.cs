using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rpgwo_Server.Networking.Packets;
using Rpgwo_Server.Events;

namespace Rpgwo_Server.Networking.Handlers
{
    public class ReqClientListHandler : PacketHandler
    {
        public ReqClientListHandler(PacketTypes packetID, Type packetType) : base(packetID, packetType)
        {
        }

        public override void Handle(NetClient client, Packet packet)
        {
            // TODO :: State Check.

            ClientListEventArgs clientListEventArgs = new ClientListEventArgs(client);

            ServerEvents.InvokeClientList(clientListEventArgs);

            // TODO :: For now we'll just send it.
        }
    }
}
