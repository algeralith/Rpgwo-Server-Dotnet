using System;
using System.Collections.Generic;
using System.Text;

namespace Rpgwo_Server.Networking.Packets
{
    public class StatCheck : Packet
    {
        public byte SkillId { get; set; }

        public StatCheck() : base((byte)PacketTypes.StatCheck, 1)
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
