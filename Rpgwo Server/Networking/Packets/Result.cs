using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpgwo_Server.Networking.Packets
{
    public class Result : Packet
    {
        public byte Catagory { get; set; }
        public Int32 Results { get; set; }
        public Int32 Data { get; set; }

        public Result() : base((byte)PacketTypes.Result, 9)
        {

        }

        public override bool Receive()
        {
            Catagory = ReadByte();
            Results = ReadInt32();
            Data = ReadInt32();

            return true;
        }
    }
}
