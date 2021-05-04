using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpgwo_Server.Networking.Packets
{
    public class Info3 : Packet
    {
        public byte Data1 { get; set; } // Represents World fog check box. 0 disabled, 1 enabled.
        public byte Data2 { get; set; } // Appears to reference in-game hour.

        public Info3() : base((byte)PacketTypes.Info3, 2)
        {

        }

        public override bool Receive()
        {
            throw new NotImplementedException();
        }
    }
}
