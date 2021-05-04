using System;
using System.Collections.Generic;
using System.Text;

namespace Rpgwo_Server.Networking.Packets
{
    public class MoveItem : Packet
    {
        public short pIndex { get; set; }
        public Int16 Xpos { get; set; }
        public Int16 Ypos { get; set; }
        public Int16 Zpos { get; set; }
        public byte MoveType { get; set; }
        public int ItemIndex { get; set; }
        public int Qty { get; set; }

        public MoveItem() : base((byte)PacketTypes.MoveItem, 29) 
        { 

        }

        public override byte[] GetBytes()
        {
            AddBytes(new byte[] { 0x00, 0x00 }); // Seems to start with two empty bytes
            AddInt16(pIndex);
            AddInt16(Xpos);
            AddInt16(Ypos);
            AddInt16(Zpos);
            AddByte(MoveType);
            AddIntAsString(ItemIndex, 9);
            AddIntAsString(Qty, 9);

            return base.GetBytes();
        }

        public override bool Receive()
        {
            throw new NotImplementedException();
        }
    }
}
