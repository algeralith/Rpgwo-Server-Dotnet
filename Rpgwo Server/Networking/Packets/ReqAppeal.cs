    using System;
using System.Collections.Generic;
using System.Text;

namespace Rpgwo_Server.Networking.Packets
{
    public class ReqAppeal:  Packet
    {
        public ReqAppeal() : base((byte)PacketTypes.RequestAppeal, 1)
        {

        }

        public override bool Receive()
        {
            throw new NotImplementedException();
        }
    }
}
