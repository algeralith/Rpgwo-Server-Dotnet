using System;
using System.Collections.Generic;
using System.Text;

namespace Rpgwo_Server.Networking.Packets
{
    public class TrainSkill : Packet
    {
        public byte SkillId { get; set; }

        public TrainSkill() : base((byte)PacketTypes.TrainSkill, 1)
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
