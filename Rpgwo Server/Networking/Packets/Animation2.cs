using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpgwo_Server.Networking.Packets
{
    public class Animation2 : Packet
    {
        public Int16 AnimationID { get; set; }
        public Int16 Xpos { get; set; }
        public Int16 Ypos { get; set; }

        public Animation2() : base((byte)PacketTypes.Animation2, 6)
        {

        }

        public override bool Receive()
        {
            AnimationID = ReadInt16();
            Xpos = ReadInt16();
            Ypos = ReadInt16();

            return true;
        }
    }
}