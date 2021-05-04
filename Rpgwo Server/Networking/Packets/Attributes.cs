using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpgwo_Server.Networking.Packets
{
    public class Attributes : Packet
    {

        public Int16 Strength { get; set; }
        public Int16 Dexterity { get; set; }
        public Int16 Quickness { get; set; }
        public Int16 Intelligence { get; set; }
        public Int16 Wisdom { get; set; }

        public Attributes() : base((byte)PacketTypes.Attributes, 10)
        {

        }

        public override bool Receive()
        {
            Strength = ReadInt16();
            Dexterity = ReadInt16();
            Quickness = ReadInt16();
            Intelligence = ReadInt16();
            Wisdom = ReadInt16();

            return true;
        }
    }
}
