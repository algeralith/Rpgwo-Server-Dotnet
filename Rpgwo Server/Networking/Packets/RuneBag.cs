using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpgwo_Server.Networking.Packets
{
    public class RuneBag : Packet
    {
        public byte[] RuneCount { get; private set; }

        public RuneBag() : base((byte)PacketTypes.RuneBag, 100)
        {
            RuneCount = new byte[100];
        }

        public override bool Receive()
        {
            RuneCount = ReadBytes(100);

            return true;
        }
    }
}
