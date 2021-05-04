using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpgwo_Server.Networking.Packets
{
    public class StartDisplay : Packet
    {
        public StartDisplay() : base((byte)PacketTypes.StartDisplay, 1)
        {

        }

        public override bool Receive()
        {
            return true;
        }
    }
}
