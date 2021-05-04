using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpgwo_Server.Networking.Packets
{
    public class Ack : Packet
    {
        public Ack() : base((byte)PacketTypes.Ack, 0)
        {

        }

        public override bool Receive()
        {
            return true;
        }
    }
}
