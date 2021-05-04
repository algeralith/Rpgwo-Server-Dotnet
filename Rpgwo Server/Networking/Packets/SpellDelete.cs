using System;
using System.Collections.Generic;
using System.Text;

namespace Rpgwo_Server.Networking.Packets
{
    public class SpellDelete : Packet
    {
        public Int16 SpellID { get; set; }

        public SpellDelete() : base((byte)PacketTypes.SpellDelete)
        {

        }

        public override byte[] GetBytes()
        {
            AddInt16(SpellID);

            return base.GetBytes();
        }

        public override bool Receive()
        {
            throw new NotImplementedException();
        }
    }
}
