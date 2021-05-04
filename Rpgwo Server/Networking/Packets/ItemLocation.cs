using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpgwo_Server.Networking.Packets
{
    public class ItemLocation : Packet
    {

        public Int16 ItemID { get; set; }
        public byte Quality { get; set; }
        public byte SpellID { get; set; }
        public byte Xpos { get; set; }
        public byte Ypos { get; set; }
        public byte Spot { get; set; } // TODO :: Figure this out.
        public Int32 Index { get; set; } // TODO ::
        public bool NoOpenSight { get; set; }
        public byte LightSource { get; set; }
        public byte MagicWeaponDamage { get; set; }
        public byte SkillBonusID { get; set; }
        public Int16 SkillBonusValue { get; set; }
        public byte NoDisplay { get; set; }
        public Int32 Quantity { get; set; }

        public ItemLocation() : base((byte)PacketTypes.ItemLocation, 25)
        {

        }

        public override bool Receive()
        {
            ItemID = ReadInt16();
            Quality = ReadByte();
            SpellID = ReadByte();
            Xpos = ReadByte();
            Ypos = ReadByte();
            Spot = ReadByte();
            Index = ReadInt32();
            NoOpenSight = Convert.ToBoolean(ReadByte());
            LightSource = ReadByte();

            // There appears to be 3 empty bytes here.
            ReadBytes(3); // TODO :: Verify

            MagicWeaponDamage = ReadByte();
            SkillBonusID = ReadByte();
            SkillBonusValue = ReadInt16();
            NoDisplay = ReadByte();
            Quantity = ReadInt32();

            return true;
        }
    }
}
