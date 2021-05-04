using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpgwo_Server.Networking.Packets
{
    public class MonsterLocation : Packet
    {
        public byte Xpos { get; set; }
        public byte Ypos { get; set; }
        public Int16 MonsterID { get; set; }
        public Int16 pIndex { get; set; }
        public byte Life { get; set; }
        public byte Stamina { get; set; }
        public byte Mana { get; set; }
        public byte Stealth { get; set; }

        public MonsterLocation() : base((byte)PacketTypes.MonsterLocation, 10)
        {

        }

        public override bool Receive()
        {
            Xpos = ReadByte();
            Ypos = ReadByte();
            MonsterID = ReadInt16();
            pIndex = ReadInt16();
            Life = ReadByte();
            Stamina = ReadByte();
            Mana = ReadByte();
            Stealth = ReadByte();

            return true;
        }
    }
}
