using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpgwo_Server.Networking.Packets
{
    public class ReqSkillDef  : Packet
    {
        // Sends a blank 0
        public ReqSkillDef() : base((byte)PacketTypes.ReqSkillDef, 1)
        {

        }

        public override bool Receive()
        {
            return true;
        }
    }
}
