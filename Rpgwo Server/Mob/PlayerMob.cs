using System;
using System.Collections.Generic;
using System.Text;
using Rpgwo_Server.Networking;

namespace Rpgwo_Server.Mob
{
    /*
        AccountName
        PlayerName
        PlayerIP
        Player X,Y,Z
        Attuned X,Y,Z
        Level
        ExtraLand
        Experience - Total, Next Level, Spendable
        Life - Current, Max, Raised #
        Stamina - Current, Max, Raised #
        Mana - Current, Max, Raise #
        Strength - Current, Raised #
        Dexterity - Current, Raised #
        Quickness - Current, Raised #
        Intelligence - Current, Raised #
        Wisdom - Current, Raised #
    */

    public class PlayerMob : Mob
    {
        private readonly NetClient _client;
        public NetClient Client => _client;


        // Life / Health / Mana
        private Raiseable _life;
        private Raiseable _mana;
        private Raiseable _stamina;

        // Strength / Dex / Quick / Int / Wisdom
        private Raiseable _strength;
        private Raiseable _dexterity;
        private Raiseable _quickness;
        private Raiseable _intelligence;
        private Raiseable _wisdom;

        public int SkillPoints { get; private set; }


        public PlayerMob()
        {

        }

        public int Life
        {
            get
            {
                return _life.Value;
            }

            init
            {
                _life = new Raiseable("Life", value, 0);
            }
        }
        public int Mana
        {
            get
            {
                return _mana.Value;
            }

            init
            {
                _mana = new Raiseable("Mana", value, 0);
            }
        }
        public int Stamina
        {
            get
            {
                return _stamina.Value;
            }

            init
            {
                _stamina = new Raiseable("Stamina", value, 0);
            }
        }
        public int Strength
        {
            get
            {
                return _strength.Value;
            }

            init
            {
                _strength = new Raiseable("Strength", value, 0);
            }
        }
        public int Dexterity
        {
            get
            {
                return _dexterity.Value;
            }

            init
            {
                _dexterity = new Raiseable("Dexterity", value, 0);
            }
        }
        public int Quickness
        {
            get
            {
                return _quickness.Value;
            }

            init
            {
                _quickness = new Raiseable("Quickness", value, 0);
            }
        }
        public int Intelligence
        {
            get
            {
                return _intelligence.Value;
            }

            init
            {
                _intelligence = new Raiseable("Intelligence", value, 0);
            }
        }
        public int Wisdom
        {
            get
            {
                return _wisdom.Value;
            }

            init
            {
                _wisdom = new Raiseable("Wisdom", value, 0);
            }
        }

        public override void TrainSkill(int skillID)
        {
            // base.TrainSkill(skillID);
        }

        public void SpecSkill(int skillID)
        {

        }

        public override bool Move(CardinalDirection direction)
        {
            return false;
        }

        public static PlayerMob CreateFromPacket(Networking.Packets.Create create)
        {
            PlayerMob playerMob = new PlayerMob()
            {
                Name = create.Name,
                Life = create.Life,
                // Stamina = create.Stamina,
                // Mana = create.Mana,
                Stamina = 0, // Stamina is Life * 2 while also being raisable.
                Mana = 0, // Mana is Wisdom * 2 while also being raisable.
                Strength = create.Strength,
                Dexterity = create.Dexterity,
                Quickness = create.Quickness,
                Intelligence = create.Intelligence,
                Wisdom = create.Wisdom,
                Head = create.Head,
                Arms = create.Arms,
                Chest = create.Chest,
                Legs = create.Legs,
                SkillPoints = WorldConfig.SkillPoints
            };

            return playerMob;
        }

    }
}
