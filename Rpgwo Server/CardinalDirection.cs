using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpgwo_Server
{
    // All of these values are just based upon num-pad key.
    public enum CardinalDirection : byte
    {
        SouthWest = 1,
        South = 2,
        SouthEast = 3,
        West = 4,
        Center = 5,
        East = 6,
        NorthWest = 7,
        North = 8,
        NorthEast = 9,
    };
}
