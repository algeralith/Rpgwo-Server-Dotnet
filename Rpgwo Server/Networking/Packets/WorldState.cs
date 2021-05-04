using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpgwo_Server.Networking.Packets
{
    public class WorldState : Packet
    {
        public Int16 MapSize { get; set; }

        public byte Year { get; set; }

        public byte Month { get; set; }

        public byte Day { get; set; }

        public byte Hour { get; set; }

        public byte Minute { get; set; }

        public byte Dark { get; set; }

        public byte Temperature { get; set; }

        public WorldState() : base((byte)PacketTypes.WorldState, 9)
        {

        }

        public override byte[] GetBytes()
        {
            AddInt16(MapSize);
            AddByte(Year);
            AddByte(Month);
            AddByte(Day);
            AddByte(Hour);
            AddByte(Minute);
            AddByte(Dark);
            AddByte(Temperature);

            return base.GetBytes();
        }

        public override bool Receive()
        {
            MapSize = ReadInt16();
            Year = ReadByte();
            Month = ReadByte();
            Day = ReadByte();
            Hour = ReadByte();
            Minute = ReadByte();
            Dark = ReadByte();
            Temperature = ReadByte();

            return true;
        }
    }
}
