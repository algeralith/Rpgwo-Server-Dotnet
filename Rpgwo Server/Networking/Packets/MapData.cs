using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpgwo_Server.Networking.Packets
{
    public class MapData : Packet
    {
        // Mapview is 19 x 17

        public Int16 Xpos { get; set; }
        public Int16 Ypos { get; set; }
        public Int16 Zpos { get; set; }
        public byte[] MapTileData { get; set; } // TODO ::
        public Int16[] Tiles { get; set; }
        public Int16[] Unknown { get; set; }
        public byte BigViewReq { get; set; }

        public MapData() : base((byte)PacketTypes.MapData, 1299) // 0x514
        {
            // MapTileData = new byte[19 * 17 * 4]; 
        }

        public override byte[] GetBytes()
        {
            for (int i = 0; i < 19 * 17; i++)
            {
                AddInt16(Tiles[i]);
            }

            for (int i = 0; i < 19 * 17; i++)
            {
                AddInt16(0);
            }

            AddInt16(Xpos);
            AddInt16(Ypos); 
            AddInt16(Zpos);

            AddByte(BigViewReq);

            return base.GetBytes();
        }

        public override bool Receive()
        {
            // MapTileData = ReadBytes(19 * 17 * 4); 
            Tiles = new Int16[19 * 17];

            for (int i = 0; i < 19 * 17; i++)
            {
                Tiles[i] = ReadInt16();
            }

            Unknown = new Int16[19 * 17];

            for (int i = 0; i < 19 * 17; i++)
            {
                Unknown[i] = ReadInt16();
            }

            Xpos = ReadInt16();
            Ypos = ReadInt16();
            Zpos = ReadInt16();

            BigViewReq = ReadByte();

            return true;
        }
    }
}
