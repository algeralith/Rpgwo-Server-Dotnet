using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpgwo_Server.Networking.Packets
{
    public class RandomByte : Packet
    { 
        public Int16 Seed { get; set; }

        public RandomByte() : base((byte)PacketTypes.RandomByte, 2)
        {
        }

        public override byte[] GetBytes()
        {
            AddInt16(Seed);

            return base.GetBytes();
        }

        public override bool Receive()
        {
            Seed = ReadInt16();
            return true;
        }
    }
}
