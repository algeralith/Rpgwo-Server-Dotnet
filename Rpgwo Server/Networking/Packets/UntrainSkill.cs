using System;
using System.Collections.Generic;
using System.Text;

namespace Rpgwo_Server.Networking.Packets
{
    public class UntrainSkill : Packet
    {
        public byte SkillId { get; set; }

        public UntrainSkill() : base((byte)PacketTypes.UntrainSkill, 1)
        {

        }

        public override bool Receive()
        {
            throw new NotImplementedException();
        }

        public override byte[] GetBytes()
        {
            AddByte(SkillId);

            return base.GetBytes();
        }
    }
}
