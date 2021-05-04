using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpgwo_Server
{
    public class Raiseable
    {
        private readonly string _name;
        public string Name => _name;
        public int RaiseCount { get; private set; } = 0;
        public int Value { get; private set; } = 0;

        public Raiseable(string name, int value, int raiseCount)
        {
            _name = name;
            Value = value;
            RaiseCount = raiseCount;
        }

        public virtual long NextRaise()
        {
            // single fact = Player(pIndex).Skill(sIndex).RaiseCount / 25 + 5
            // Player(pIndex).Skill(sIndex).XP = ((fact * Player(pIndex).Skill(sIndex).RaiseCount ^ 2) + Player(pIndex).Skill(sIndex).RaiseCount * fact) + 100
            double fact = RaiseCount / 25 + 5;
            return (long)((Math.Pow(fact * RaiseCount, 2) + RaiseCount * fact) + 100.0); 
        }

        public virtual void Raise()
        {
            RaiseCount += 1;
        }
    }
}
