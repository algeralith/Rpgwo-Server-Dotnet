using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rpgwo_Server.Networking.Packets;

namespace Rpgwo_Server.Networking.Handlers
{
    public class RequestHandler : PacketHandler
    {
        public RequestHandler(PacketTypes packetID, Type packetType) : base(packetID, packetType)
        {

        }

        public override void Handle(NetClient client, Packet packet)
        { 
            // TODO :: 

        }
    }
}
