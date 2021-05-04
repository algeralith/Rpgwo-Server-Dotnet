using System;
using System.Collections.Generic;
using System.Text;

namespace Rpgwo_Server.Networking.Packets
{
    public class StartLineDisplay : Packet
    {
        public byte Direction { get; set; }

        public StartLineDisplay() : base((byte)PacketTypes.StartLineDisplay, 1)
        {

        }

        public override byte[] GetBytes()
        {
            AddByte((byte)Direction);

            return base.GetBytes();
        }

        public override bool Receive()
        {
            Direction = ReadByte();

            return true;
        }
    }
}
