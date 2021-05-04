using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpgwo_Server.Networking.Packets
{
    public class ReqPlayerList : Packet
    {
        public ReqPlayerList() : base((byte)PacketTypes.ReqPlayerList, 1)
        {

        }

        public override bool Receive()
        {
            return true;
        }
    }
}
