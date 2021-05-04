using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpgwo_Server.Items
{
    public class ItemDef
    {
        public bool InUse { get; set; }
        public String Name { get; set; } // 30 Characters
        public Int16[] Animation { get; set; } = new Int16[10];
        public ImageType ImageType { get; set; }
        public ItemClass ItemClass { get; set; }
        public Int16 Burden { get; set; }
        public Int32 Value { get; set; }
        public bool OpenSightLine { get; set; }
        public bool BlockMovement { get; set; }
        public Int16 Food { get; set; }
        public Int16 Water { get; set; }
        public Int16 FoodLife { get; set; }
        public Int16 FoodStamina { get; set; }
        public Int16 FoodMana { get; set; }
        public Int16 PoisonDamage { get; set; }
        public Int16 PoisonCure { get; set; }
        public bool Readable { get; set; }
        public bool Stackable { get; set; }
        public Int16 Rarity { get; set; }
        public Int16 LightSource { get; set; }
        public Int16 WeaponMinRange { get; set; }
        public Int16 WeaponMaxRange { get; set; }
        public Int16 CombatSkillID { get; set; }
        public Int16 DamageLow { get; set; }
        public Int16 DamageHigh { get; set; }
        public Single AttackSpeed { get; set; }
        public bool MissleWeapon { get; set; }
        public Single MagicPower { get; set; }
        public Int16 EssenceSteal { get; set; } 
        public bool TwoHanded { get; set; }
        public bool IgnoreShields { get; set; }
        public bool IgnoreArmor { get; set; }
        public Int16 WeaponAL { get; set; }
        public bool BreakShield { get; set; }
        public bool StaminaDamage { get; set; }
        public Int16 CriticalBonus { get; set; }
        public Int16 SkillReq { get; set; }
        public ArmorSpot ArmorSpot { get; set; }
        public Int16 ArmorLevel { get; set; }
        public Int16 MagicArmorLevel { get; set; }
        public WeaponDamageType WeaponDamageType { get; set; }
        public Int16 CutAL { get; set; }
        public Int16 BashAL { get; set; }
        public Int16 ThrustAL { get; set; }
        public Int16 FireAL { get; set; }
        public Int16 ColdAL { get; set; }
        public Int16 ElectricalAL { get; set; }
        public Int16 Warmth { get; set; }
        public bool IsAmmo { get; set; }
        public bool SelfRepair { get; set; }
        public bool DynamicDamage { get; set; }
    }
}
