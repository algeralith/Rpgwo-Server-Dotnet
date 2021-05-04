    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpgwo_Server.Networking.Packets
{
    public class CreateDef : Packet
    {
        public int SkillPoints { get; set; }
        public int Attributes { get; set; }

        public CreateDef() : base((byte)PacketTypes.CreateDef, 4)
        {

        }

        public override byte[] GetBytes()
        {
            AddInt16((Int16)SkillPoints);
            AddInt16((Int16)Attributes);

            return base.GetBytes();
        }

        public override bool Receive()
        {
            SkillPoints = ReadInt16();
            Attributes = ReadInt16();

            return true;
        }
    }
}
