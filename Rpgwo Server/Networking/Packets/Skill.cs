using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpgwo_Server.Networking.Packets
{
    public class Skill : Packet
    {
        public string SkillName { get; set; }

        public byte SkillID { get; set; }

        public bool ClearList { get; set; } // Probably a bool, verify. TODO ::

        public Int16 Value { get; set; }

        public byte SkillPoints { get; set; }

        public byte Strength { get; set; }

        public byte Dexterity { get; set; }

        public byte Quickness { get; set; }

        public byte Intelligence { get; set; }

        public byte Wisdom { get; set; }

        public byte Divisor { get; set; }

        public byte Status { get; set; }

        public byte Spec { get; set; } // Probably 0x00 and 0x01. TODO :: Verify.

        public Skill() : base((byte)PacketTypes.Skill, 33)
        {

        }

        public override bool Receive()
        {
            SkillName = ReadString(20);
            SkillID = ReadByte();
            ClearList = ReadBool();
            Value = ReadInt16();
            SkillPoints = ReadByte();
            Strength = ReadByte();
            Dexterity = ReadByte();
            Quickness = ReadByte();
            Intelligence = ReadByte();
            Wisdom = ReadByte();
            Divisor = ReadByte();
            Status = ReadByte();
            Spec = ReadByte(); // 0 and 1

            return true;
        }
    }
}
