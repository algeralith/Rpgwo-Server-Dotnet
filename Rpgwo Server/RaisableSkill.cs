using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpgwo_Server
{
    public class RaisableSkill : Raiseable
    {

        public RaisableSkill(string name, int value, int raiseCount) : base(name, value, raiseCount)
        {

        }

        public override long NextRaise()
        {
            throw new NotImplementedException();
        }
    }
}
