using System;
using System.Collections.Generic;
using System.Text;

namespace Rpgwo_Server.Networking.Packets
{
    public class Cast : Packet
    {
        public Int16 SpellID { get; set; }
        public byte Rune1 { get; set; }
        public byte Rune2 { get; set; }
        public byte Rune3 { get; set; }
        public byte Rune4 { get; set; }
        public byte Rune5 { get; set; }
        public Int16 PlayerIndex { get; set; }
        public int MapItemIndex { get; set; }
        public byte CarryIndex { get; set; }
        public Int16 Xpos { get; set; }
        public Int16 Ypos { get; set; }
        public Int16 Zpos { get; set; }

        public Cast() : base((byte)PacketTypes.Cast, 25)
        {

        }

        public override byte[] GetBytes()
        {
            AddInt16(SpellID);
            AddByte(Rune1);
            AddByte(Rune2);
            AddByte(Rune3);
            AddByte(Rune4);
            AddByte(Rune5);
            AddInt16(PlayerIndex);
            AddIntAsString(MapItemIndex, 9);
            AddByte(CarryIndex);
            AddInt16(Xpos);
            AddInt16(Ypos);
            AddInt16(Zpos);

            return base.GetBytes();
        }

        public override bool Receive()
        {
            throw new NotImplementedException();
        }
    }
}
