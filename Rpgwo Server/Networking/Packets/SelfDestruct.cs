using System;
using System.Collections.Generic;
using System.Text;

namespace Rpgwo_Server.Networking.Packets
{
    public class SelfDestruct: Packet
    {
        public SelfDestruct() : base ((byte)PacketTypes.SelfDestruct, 1)
        {

        }

        public override bool Receive()
        {
            throw new NotImplementedException();
        }
    }
}
