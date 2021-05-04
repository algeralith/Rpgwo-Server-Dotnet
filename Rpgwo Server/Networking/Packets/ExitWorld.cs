using System;
using System.Collections.Generic;
using System.Text;

namespace Rpgwo_Server.Networking.Packets
{
    public class ExitWorld : Packet
    {
        public ExitWorld() : base((byte)PacketTypes.ExitWorld, 1)
        {

        }

        public override bool Receive()
        {
            throw new NotImplementedException();
        }
    }
}
