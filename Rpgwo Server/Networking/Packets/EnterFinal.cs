using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpgwo_Server.Networking.Packets
{
    public class EnterFinal : Packet
    {
        public EnterFinal() : base((byte)PacketTypes.EnterFinal, 1)
        {

        }

        public override byte[] GetBytes()
        {
            return base.GetBytes();
        }

        public override bool Receive()
        {
            return true;
        }
    }
}
