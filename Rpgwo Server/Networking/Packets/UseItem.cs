using System;
using System.Collections.Generic;
using System.Text;

namespace Rpgwo_Server.Networking.Packets
{
    public class UseItem : Packet
    {
        public int ItemIndex { get; set; }
        public UseType UseType { get; set; }
        public int ItemIndex2 { get; set; }
        public Int16 UseID { get; set; }
        public Int16 Xpos { get; set; }
        public Int16 Ypos { get; set; }
        public Int16 Data1 { get; set; }
        public Int16 Data2 { get; set; }
        public Int16 Data3 { get; set; }
        public Int16 Data4 { get; set; }


        public UseItem() : base((byte)PacketTypes.UseItem, 33)
        {

        }

        public override byte[] GetBytes()
        {
            AddIntAsString(ItemIndex, 9);
            AddByte((byte)UseType);
            AddIntAsString(ItemIndex2, 9);
            AddInt16(UseID);
            AddInt16(Xpos);
            AddInt16(Ypos);
            AddInt16(Data1);
            AddInt16(Data2);
            AddInt16(Data3);
            AddInt16(Data4);

            return base.GetBytes();
        }

        public override bool Receive()
        {
            throw new NotImplementedException();
        }
    }

    public enum UseType
    {
        Eat = 0,
        Merge = 1,
        Split = 2,
        AutoStack = 3,
        ItemOnMapItem = 10,
        ItemOnMap = 20,
        ItemOnPlayerItem = 30,
        HandOnMapItem = 40,
        HandOnItem = 50,
        HandOnSlot = 60,
        ItemOnPlayer = 70
    }

}
