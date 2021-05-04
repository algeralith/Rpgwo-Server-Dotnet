using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpgwo_Server.Networking.Packets
{
    public class PlayerStats : Packet
    {
        // Life
        public int Life { get; set; }
        // Max Life
        public int MaxLife { get; set; }
        // Stamina
        public int Stamina { get; set; }
        // Max Stamina
        public int MaxStamina { get; set; }
        // Total Exp
        public int TotalExp { get; set; }
        // Earned Exp
        public int EarnedExp { get; set; } // Spendable
        // Mana
        public int Mana { get; set; }
        // Max Mana
        public int MaxMana { get; set; }
        // Level
        public int Level { get; set; }
        // Next Level
        public int NextLevel { get; set; }
        // Vitae
        public byte Vitae { get; set; }
        // Vitae Exp
        public int VitaeExp { get; set; }
        // Poison
        public byte Poison { get; set; }

        public PlayerStats() : base((byte)PacketTypes.PlayerStats, 70)
        {

        }

        public override bool Receive()
        {
            Life = ReadStringAsInt(4);
            MaxLife = ReadStringAsInt(4);

            Stamina = ReadStringAsInt(4);
            MaxStamina = ReadStringAsInt(4);

            TotalExp = ReadStringAsInt(9);
            EarnedExp = ReadStringAsInt(9);

            Mana = ReadStringAsInt(4);
            MaxMana = ReadStringAsInt(4);

            Level = ReadStringAsInt(4);

            NextLevel = ReadStringAsInt(9);

            Vitae = ReadByte();

            // The next three bytes do not appear to be used.
            ReadBytes(3);

            VitaeExp = ReadStringAsInt(9);

            // Next Byte is not used.
            ReadByte();

            Poison = ReadByte(); 

            return true;
        }
    }
}
