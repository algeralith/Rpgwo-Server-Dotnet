using System;
using System.Collections.Generic;
using System.Text;

namespace Rpgwo_Server.Networking.Packets
{
    public class CheckItem  : Packet
    {
        public byte ItemSpot { get; set; }

        public CheckItem() : base((byte)PacketTypes.CheckItem, 1)
        {

        }

        public override bool Receive()
        {
            throw new NotImplementedException();
        }

        public override byte[] GetBytes()
        {
            AddByte(ItemSpot);

            return base.GetBytes();
        }
    }
}
