using System;
using System.Collections.Generic;
using System.Text;

namespace Rpgwo_Server.Networking.Packets
{
    public  class DropRune : Packet
    {
        public byte RuneIndex { get; set; }
        public byte RuneDropLocation { get; set; }

        public DropRune() : base((byte)PacketTypes.DropRune, 2)
        {

        }

        public override byte[] GetBytes()
        {
            AddByte(RuneIndex);
            AddByte((byte)RuneDropLocation);

            return base.GetBytes();
        }

        public override bool Receive()
        {
            throw new NotImplementedException();
        }
    }

    public enum RuneDropLocation
    {
        Ground= 0,
        Inventory = 1
    }
}
