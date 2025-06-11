using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipAppLibrary.Models
{
    // 0 = empty, 1 = Ship, 2 = Miss, 3 = Hit, 4 = sunk
    public enum GridSpotStatus
    {
        Empty,
        Ship,
        Miss,
        Hit,
        Sunk
    }
}
