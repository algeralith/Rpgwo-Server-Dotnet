using System;
using System.Collections.Generic;
using System.Text;

namespace Rpgwo_Server.Mob
{
    public class Mob
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Int16 X { get; set; } = 100;
        public Int16 Y { get; set; } = 100;
        public Int16 Z { get; set; } = 0;
        public Int16 Level { get; set; }

        // Paper Doll images. Not really used except on clones
        public byte Head { get; set; }
        public byte Arms { get; set; }
        public byte Chest { get; set; }
        public byte Legs { get; set; }

        public Dictionary<int, RaisableSkill> _skill = new Dictionary<int, RaisableSkill>(); 

        public virtual bool Move(CardinalDirection direction)
        {
            return false;
        }

        public virtual void TrainSkill(int skillID)
        {

        }
    }
}
