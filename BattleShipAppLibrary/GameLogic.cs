using BattleShipAppLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipAppLibrary
{
    public static class GameLogic
    {
        public static int GetShotCount(PlayerInfoModel winner)
        {
            return winner.ShotGrid.Where((spot) => { return spot.Status != GridSpotStatus.Empty; }).Count();
        }

        public static bool IdentifyShotResult(PlayerInfoModel opponent, string row, int column)
        {
            bool output = false;

            GridSpotModel? spot = opponent.ShipLocations.Where((spot) => { return spot.SpotLetter == row && spot.SpotNumber == column; }).FirstOrDefault();

            if (spot != null)
            {
                if (spot.Status == GridSpotStatus.Ship)
                {
                    spot.Status = GridSpotStatus.Sunk;
                    output = true;
                }
            }

            return output;
        }

        public static void InitializeGrid(PlayerInfoModel model)
        {
            // newer method of initializing  List of strings
            //List<string> letters = ["A", "B", "C", "D", "E"];

            List<string> letters = new List<string>
            {
                "A",
                "B",
                "C",
                "D",
                "E"
            };

            List<int> numbers = new List<int>
            {
                1, 2, 3, 4, 5
            };

            foreach (string letter in letters)
            {
                foreach(int number in numbers)
                {
                    AddGridSpot(model, letter, number);
                }
            }
        }

        public static void MarkShotResult(PlayerInfoModel activePlayer, string row, int column, bool isAHit)
        {
            GridSpotModel? gridSpot = RetrieveShotGridSpot(activePlayer, row, column);

            if (isAHit)
            {
                gridSpot.Status = GridSpotStatus.Hit;
            }
            else
            {
                gridSpot.Status = GridSpotStatus.Miss;
            }
        }

        public static bool PlaceShip(PlayerInfoModel model, string? location)
        {
            bool output = false;

            if (location.Length > 1)
            {
                (string row, int column) = SplitShotIntoRowAndColumn(location);
                GridSpotModel? spot = RetrieveShotGridSpot(model, row, column);

                if (spot != null)
                {
                    GridSpotModel ship = new GridSpotModel { SpotLetter = row, SpotNumber = column, Status = GridSpotStatus.Ship };
                    model.ShipLocations.Add(ship);
                    output = true;
                }
            }

            return output;
        }

        public static bool PlayerStillActive(PlayerInfoModel opponent)
        {
            bool output;

            output = opponent.ShipLocations.Any(ship => { return ship.Status == GridSpotStatus.Ship; });

            return output;
        }

        public static (string row, int column) SplitShotIntoRowAndColumn(string shot)
        {
            throw new NotImplementedException();
        }

        public static bool ValidateShot(PlayerInfoModel activePlayer, string row, int column)
        {
            throw new NotImplementedException();
        }

        private static void AddGridSpot(PlayerInfoModel model, string letter, int number)
        {
            GridSpotModel spot = new GridSpotModel
            {
                SpotLetter = letter,
                SpotNumber = number,
                Status = GridSpotStatus.Empty
            };

            model.ShotGrid.Add(spot);
        }

        private static GridSpotModel? RetrieveShotGridSpot(PlayerInfoModel model, string row, int column)
        {
            return model.ShotGrid.Where((spot) => { return spot.SpotLetter == row && spot.SpotNumber == column; }).FirstOrDefault();
        }
    }
}
