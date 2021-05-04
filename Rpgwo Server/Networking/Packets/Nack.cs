using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpgwo_Server.Networking.Packets
{
    public class Nack : Packet
    {
        public Nack() : base((byte)PacketTypes.Nack, 0)
        {

        }

        public override bool Receive()
        {
            return true;
        }
    }
}
