using System;
using System.Collections.Generic;
using System.Text;

namespace Rpgwo_Server.Networking.Packets
{
    public class MapLineData : Packet
    {
        public Int16 Xpos { get; set; }
        public Int16 Ypos { get; set; }
        public Int16 Zpos { get; set; }
        public byte Direction { get; set; }
        public Int16[] Tiles { get; set; }


        public MapLineData() : base((byte)PacketTypes.MapLineData, 45)
        {

        }

        public override byte[] GetBytes()
        {
            AddInt16(Xpos);
            AddInt16(Ypos);
            AddInt16(Zpos);
            AddByte(Direction);

            // TODO :: Sanitation checks.
            for (int i = 0; i < Tiles.Length; i++)
                AddInt16(Tiles[i]);

            return base.GetBytes();
        }

        public override bool Receive()
        {
            Xpos = ReadInt16();
            Ypos = ReadInt16();
            Zpos = ReadInt16();
            Direction = ReadByte();

            Tiles = new Int16[19]; // TODO :: This should be a constant somewhere;

            for (int i = 0; i < 19; i++)
                Tiles[i] = ReadInt16();

            return true;
        }
    }
}
