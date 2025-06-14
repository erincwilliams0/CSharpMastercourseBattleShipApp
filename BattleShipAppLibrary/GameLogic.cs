using BattleShipAppLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipAppLibrary
{
    public static class GameLogic
    {
        public static int GetShotCount(PlayerInfoModel player)
        {
            return player.ShotGrid.Where((spot) => { return spot.Status != GridSpotStatus.Empty; }).Count();
        }

        public static bool IdentifyShotResult(PlayerInfoModel opponent, string row, int column)
        {
            bool isAHit = false;

            GridSpotModel? spot = opponent.ShipLocations.Where((spot) => { return spot.SpotLetter == row && spot.SpotNumber == column; }).FirstOrDefault();

            if (spot != null)
            {
                if (spot.Status == GridSpotStatus.Ship)
                {
                    spot.Status = GridSpotStatus.Sunk;
                    isAHit = true;
                }
            }

            return isAHit;
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

        public static void MarkShotResult(PlayerInfoModel player, string row, int column, bool isAHit)
        {
            GridSpotModel? gridSpot = RetrieveShotGridSpot(player, row, column);

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
            (string row, int column) = SplitShotIntoRowAndColumn(location);

            bool isValidLocation = ValidateGridLocation(model, row, column);
            bool isSpotOpen = ValidateShipLocation(model, row, column);
            
            if (isValidLocation && isSpotOpen)
            {
                model.ShipLocations.Add(new GridSpotModel { SpotLetter = row.ToUpper(), SpotNumber = column, Status = GridSpotStatus.Ship });
                output = true;
            }

            return output;
        }

        private static bool ValidateShipLocation(PlayerInfoModel model, string row, int column)
        {
            bool isValidLocation = true;

            foreach (var ship in model.ShipLocations)
            {
                if (ship.SpotLetter == row.ToUpper() && ship.SpotNumber == column)
                {
                    isValidLocation = false;
                }
            }
            return isValidLocation;
        }

        private static bool ValidateGridLocation(PlayerInfoModel model, string row, int column)
        {
            bool isValidLocation = false;

            foreach (var ship in model.ShotGrid)
            {
                if (ship.SpotLetter == row.ToUpper() && ship.SpotNumber == column)
                {
                    isValidLocation = true;
                }
            }
            return isValidLocation;
        }

        public static bool PlayerStillActive(PlayerInfoModel opponent)
        {
            bool isActive;

            isActive = opponent.ShipLocations.Any(ship => { return ship.Status == GridSpotStatus.Ship; });

            return isActive;
        }

        public static (string row, int column) SplitShotIntoRowAndColumn(string shot)
        {
            string row = "";
            int column = 0;

            if (shot.Length != 2)
            {
                throw new ArgumentException("This was an invalid shot type.", "shot");
            }

            char[] shotArray = shot.ToArray();
            row = shotArray[0].ToString();
            column = int.Parse(shotArray[1].ToString());

            return (row, column);
        }

        public static bool ValidateShot(PlayerInfoModel player, string row, int column)
        {
            bool isValidShot = true;

            foreach (var gridSpot in player.ShotGrid)
            {
                if (gridSpot.SpotLetter == row.ToUpper() && gridSpot.SpotNumber == column)
                {
                    if (gridSpot.Status == GridSpotStatus.Empty)
                    {
                        isValidShot = true;
                    }
                }
            }
            return isValidShot;
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
