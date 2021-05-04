using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Rpgwo_Server.Networking.Packets
{
    public class SpellDef : Packet
    {
        
        public Int16 SpellID { get; set; }
        public Int16 ManaCost { get; set; }
        public byte Target { get; set; }
        public byte Range { get; set; }
        public byte LOS { get; set; }
        public string Name { get; set; } // 30 Characters
        public string Description { get; set; } // 100 Characters
        public byte Found { get; set; }
        public Int16 Rune1 { get; set; }
        public Int16 Rune2 { get; set; }
        public Int16 Rune3 { get; set; }
        public Int16 Rune4 { get; set; }
        public Int16 Rune5 { get; set; }

        public SpellDef() : base((byte)PacketTypes.SpellDef, 153)
        {

        }

        public override bool Receive()
        {
            SpellID = ReadInt16();
            ReadBytes(5); // Empty Bytes
            ManaCost = ReadInt16();
            Target = ReadByte();
            Range = ReadByte();
            LOS = ReadByte();
            Name = ReadString(30);
            Description = ReadString(100);
            Found = ReadByte();
            Rune1 = ReadInt16();
            Rune2 = ReadInt16();
            Rune3 = ReadInt16();
            Rune4 = ReadInt16();

            return true;            
        }
    }
}
