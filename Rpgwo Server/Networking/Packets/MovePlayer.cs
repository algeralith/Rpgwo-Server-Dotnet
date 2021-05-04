using System;
using System.Collections.Generic;
using System.Text;

namespace Rpgwo_Server.Networking.Packets
{
    public class MovePlayer : Packet
    {
        public byte Direction { get; set; }
        public byte ViewOffset { get; set; }

        public MovePlayer() : base((byte)PacketTypes.MovePlayer, 2)
        {

        }

        public override byte[] GetBytes()
        {
            AddByte((byte)Direction);
            AddByte((byte)ViewOffset);

            return base.GetBytes();
        }

        public override bool Receive()
        {
            Direction = ReadByte();
            ViewOffset = ReadByte();

            return true;
        }

    }
}
