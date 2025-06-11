using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipAppLibrary.Models
{
    public class PlayerInfoModel
    {
        public string UserName { get; set; }
        public List<GridSpotModel> ShipLocations { get; set; }
        public List<GridSpotModel> HitLocations { get; set; }
    }
}
