using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpgwo_Server.Networking.Packets
{
    public class Info2 : Packet
    {
        public byte[] Data1;

        public byte[] Data2;

        public Info2() : base((byte)PacketTypes.Info2, 120)
        {

        }

        public override byte[] GetBytes()
        {
            return base.GetBytes();
        }

        public override bool Receive()
        {
            Data1 = ReadBytes(60);
            Data2 = ReadBytes(60);

            return true;
        }
    }
}
    