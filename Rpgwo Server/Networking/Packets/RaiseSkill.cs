using System;
using System.Collections.Generic;
using System.Text;

namespace Rpgwo_Server.Networking.Packets
{
    public class RaiseSkill: Packet
    {
        public byte SkillId { get; set; }

        public RaiseSkill() : base((byte)PacketTypes.RaiseSkill, 1)
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
