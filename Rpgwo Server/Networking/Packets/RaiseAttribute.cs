using System;
using System.Collections.Generic;
using System.Text;

namespace Rpgwo_Server.Networking.Packets
{
    public class RaiseAttribute :  Packet
    {
        public byte Attribute { get; set; }

        public RaiseAttribute() : base((byte)PacketTypes.RaiseAttribute,  1)
        {

        }

        public override bool Receive()
        {
            throw new NotImplementedException();
        }

        public override byte[] GetBytes()
        {
            AddByte(Attribute);

            return base.GetBytes();
        }
    }
}
