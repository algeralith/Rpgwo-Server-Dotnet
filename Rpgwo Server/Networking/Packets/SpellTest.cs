using System;
using System.Collections.Generic;
using System.Text;

namespace Rpgwo_Server.Networking.Packets
{
    public class SpellTest : Packet
    {
        public byte Rune1 { get; set; }
        public byte Rune2 { get; set; }
        public byte Rune3 { get; set; }
        public byte Rune4 { get; set; }
        public byte Rune5 { get; set; }

        public SpellTest() : base((byte)PacketTypes.SpellTest, 5)
        {

        }

        public override byte[] GetBytes()
        {
            AddByte(Rune1);
            AddByte(Rune2);
            AddByte(Rune3);
            AddByte(Rune4);
            AddByte(Rune5);

            return base.GetBytes();
        }

        public override bool Receive()
        {
            throw new NotImplementedException();
        }
    }
}
