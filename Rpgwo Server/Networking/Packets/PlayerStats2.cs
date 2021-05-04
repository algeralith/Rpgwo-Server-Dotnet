using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpgwo_Server.Networking.Packets
{
    public class PlayerStats2 : Packet
    {
        public Int16 Burden { get; set; }
        public Int16 MaxBurden { get; set; }
        public Int16 Food { get; set; }
        public Int16 MaxFood { get; set; }
        public Int16 Water { get; set; }
        public Int16 MaxWater { get; set; }

        public PlayerStats2() : base((byte)PacketTypes.PlayerStats2, 12)
        {

        }

        public override bool Receive()
        {
            Burden = ReadInt16();
            MaxBurden = ReadInt16();
            Food = ReadInt16();
            MaxFood = ReadInt16();
            Water = ReadInt16();
            MaxWater = ReadInt16();

            return true;
        }
    }
}
