using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpgwo_Server.Items
{
    public enum WeaponDamageType : Int16
    {
        Cut = 1,
        Bash = 2,
        Thrust = 3,
        Fire = 4,
        Cold = 5,
        Electric = 6,
        CutThrust = 7,
        Magic = 8,
        CutThrustBash = 9
    }
}
