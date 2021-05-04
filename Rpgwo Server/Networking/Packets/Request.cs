using System;
using System.Collections.Generic;
using System.Text;

namespace Rpgwo_Server.Networking.Packets
{
    public class Request : Packet
    {
        public byte Category { get; set; }
        public Int32 Data1 { get; set; }
        public Int32 Data2 { get; set; }
        public Int32 Data3 { get; set; }

        public Request() : base((byte)PacketTypes.Request, 13)
        {

        }

        public override byte[] GetBytes()
        {
            AddByte(Category);
            AddInt32(Data1);
            AddInt32(Data2);
            AddInt32(Data3);

            return base.GetBytes();
        }

        public override bool Receive()
        {
            Category = ReadByte();
            Data1 = ReadInt32();
            Data2 = ReadInt32();
            Data3 = ReadInt32();

            return true;
        }
    }
}
