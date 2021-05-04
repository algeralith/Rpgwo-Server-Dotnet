using System;
using System.Collections.Generic;
using System.Text;

namespace Rpgwo_Server.Networking.Packets
{
    public class PlayerInfo2 : Packet
    {
        public String Name { get; set; }
        
        public Int16 Level { get; set; }
        public Int16 Life { get; set; }
        public Int16 Stamina { get; set; }
        public Int16 Mana { get; set; }

        // There seems to be an empty byte following mana.

        public Int16 MeleeAttack { get; set; }
        public Int16 MeleeDefense { get; set; }
        public Int16 MagicDefese { get; set; }
        public Int16 MagicAttack { get; set; }
        public Int16 MissileAttack { get; set; }
        public Int16 MissileDefense { get; set; }

        public String Guild { get; set; }

        public Int16 WeaponID { get; set; }
        public Int16 ArmorID { get; set; }
        public Int16 ShieldID { get; set; }
        public Int16 HeadID { get; set; }
        public Int16 LegID { get; set; }

        public bool PK { get; set; }

        public Int16 Image { get; set; }

        public String Owner { get; set; }


        public PlayerInfo2(): base((byte)PacketTypes.PlayerInfo2, 164)
        {

        }

        public override bool Receive()
        {
            Name = ReadString(50);

            Level = ReadInt16();
            Life = ReadInt16();
            Stamina = ReadInt16();
            Mana = ReadInt16();

            ReadByte(); // There appears to be an empty byte between Mana and MeleeAttack.

            MeleeAttack = ReadInt16();
            MeleeDefense = ReadInt16();
            MagicDefese = ReadInt16();
            MagicAttack = ReadInt16();
            MissileAttack = ReadInt16();
            MissileDefense = ReadInt16();

            Guild = ReadString(30);

            WeaponID = ReadInt16();
            ArmorID = ReadInt16();
            ShieldID = ReadInt16();
            HeadID = ReadInt16();
            LegID = ReadInt16();

            PK = ReadBool();

            Image = ReadInt16();

            Owner = ReadString(50);

            return true;
        }
    }
}
