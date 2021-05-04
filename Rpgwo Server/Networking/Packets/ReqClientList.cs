using System;
using System.Collections.Generic;
using System.Text;

namespace Rpgwo_Server.Networking.Packets
{
    public class ReqClientList : Packet
    {
        public ReqClientList() : base((byte)PacketTypes.ReqClientList, 1)
        {

        }

        public override bool Receive()
        {
            return true;
        }
    }
}
