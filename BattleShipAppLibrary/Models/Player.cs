using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipAppLibrary.Models
{
    public class Player
    {
        public string PlayerName { get; set; }
        public List<GridSpot> ShipLocations { get; set; }
        public List<GridSpot> HitLocations { get; set; }
    }
}
