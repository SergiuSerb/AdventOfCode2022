using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day14.Models
{
    public class Map
    {
        public static IList<IPlaceable> items;
        public static int floorY = 0;
        private static SandSpawn _sandSpawn;
        
        public static int RockCount { get; set; }

        public Map()
        {
            items = new List<IPlaceable>();
        }
        
        public static void AddSandSpawn(SandSpawn sandSpawn)
        {
            items.Add(sandSpawn);
            _sandSpawn = sandSpawn;
        }

        private static bool IsAreaBelowEmpty(int coordinatesRow, int coordinatesColumn)
        {
            return !items.Any(x => x.CoordinatesRow == coordinatesRow + 1 && x.CoordinatesColumn == coordinatesColumn);
        }

        private static bool IsAreaBelowLeftEmpty(int coordinatesRow, int coordinatesColumn)
        {
            return !items.Any(x => x.CoordinatesRow == coordinatesRow + 1 && x.CoordinatesColumn == coordinatesColumn - 1);
        }

        private static bool IsAreaAboveLeftEmpty(int coordinatesRow, int coordinatesColumn)
        {
            return !items.Any(x => x.CoordinatesRow == coordinatesRow - 1 && x.CoordinatesColumn == coordinatesColumn - 1);
        }

        private static bool IsAreaAboveRightEmpty(int coordinatesRow, int coordinatesColumn)
        {
            return !items.Any(x => x.CoordinatesRow == coordinatesRow - 1 && x.CoordinatesColumn == coordinatesColumn + 1);
        }

        private static bool IsAreaBelowRightEmpty(int coordinatesRow, int coordinatesColumn)
        {
            return !items.Any(x => x.CoordinatesRow == coordinatesRow + 1 && x.CoordinatesColumn == coordinatesColumn + 1);
        }

        private static bool IsAreaAboveEmpty(int coordinatesRow, int coordinatesColumn)
        {
            return !items.Any(x => x.CoordinatesRow == coordinatesRow - 1 && x.CoordinatesColumn == coordinatesColumn);
        }

        private static bool IsAreaRightEmpty(int coordinatesRow, int coordinatesColumn)
        {
            return !items.Any(x => x.CoordinatesRow == coordinatesRow && x.CoordinatesColumn == coordinatesColumn + 1);
        }

        private static bool IsAreaLeftEmpty(int coordinatesRow, int coordinatesColumn)
        {
            return !items.Any(x => x.CoordinatesRow == coordinatesRow && x.CoordinatesColumn == coordinatesColumn - 1);
        }

        private static bool CanBeDeleted( IPlaceable sand )
        {
            bool isAreaBelowEmpty = false;
            bool isAreaAboveEmpty = false;
            bool isAreaLeftEmpty = false;
            bool isAreaRightEmpty = false;
            bool isAreaAboveRightEmpty = false;
            bool isAreaAboveLeftEmpty = false;

            Parallel.Invoke( () => isAreaBelowEmpty = IsAreaBelowEmpty( sand.CoordinatesRow, sand.CoordinatesColumn ),
                            () => isAreaAboveEmpty = IsAreaAboveEmpty( sand.CoordinatesRow, sand.CoordinatesColumn ),
                            () => isAreaLeftEmpty = IsAreaLeftEmpty( sand.CoordinatesRow, sand.CoordinatesColumn ),
                            () => isAreaRightEmpty = IsAreaRightEmpty( sand.CoordinatesRow, sand.CoordinatesColumn ),
                            () => isAreaAboveRightEmpty = IsAreaAboveRightEmpty( sand.CoordinatesRow, sand.CoordinatesColumn ),
                            () => isAreaAboveLeftEmpty = IsAreaAboveLeftEmpty( sand.CoordinatesRow, sand.CoordinatesColumn )
                           );
            
            return !isAreaAboveEmpty && !isAreaBelowEmpty && !isAreaLeftEmpty &&
                    !isAreaRightEmpty && !isAreaAboveRightEmpty && !isAreaAboveLeftEmpty;
        }        
        
        public static void Print()
        {
            int boundingBoxMinRow = items.Min(x => x.CoordinatesRow);
            int boundingBoxMaxRow = items.Max(x => x.CoordinatesRow);
            int rowOffset = boundingBoxMinRow;
            
            int boundingBoxMinColumn = items.Min(x => x.CoordinatesColumn);
            int boundingBoxMaxColumn = items.Max(x => x.CoordinatesColumn);
            int columnOffset = boundingBoxMinColumn;

            char[,] visualRepresentation = new char[boundingBoxMaxRow - rowOffset + 1, boundingBoxMaxColumn - columnOffset + 1];

            for (int i = 0; i < visualRepresentation.GetLength(0); i++)
            {
                for (int j = 0; j < visualRepresentation.GetLength(1); j++)
                {
                    visualRepresentation[i,j] = '.';
                }
            }

            foreach (IPlaceable placeable in items)
            {
                if (placeable is SettledSand)
                {
                    visualRepresentation[placeable.CoordinatesRow - rowOffset, placeable.CoordinatesColumn - columnOffset] = 'O';
                }

                if (placeable is Rock)
                {
                    visualRepresentation[placeable.CoordinatesRow - rowOffset, placeable.CoordinatesColumn - columnOffset] = '#';
                }
                
                if (placeable is SandSpawn)
                {
                    visualRepresentation[placeable.CoordinatesRow - rowOffset, placeable.CoordinatesColumn - columnOffset] = '+';
                }
            }
            
            for (int i = 0; i < visualRepresentation.GetLength(0); i++)
            {
                for (int j = 0; j < visualRepresentation.GetLength(1); j++)
                {
                    Console.Write(visualRepresentation[i, j]);
                }
                
                Console.WriteLine();
            }
            
        }

        public static void DestructNonSignificantPlaceables()
        {
            IList<IPlaceable> placeablesToDestroy = items.Where(  CanBeDeleted ).ToList();

            int beforeOptimizationCount = items.Count;
            
            foreach ( IPlaceable placeable in placeablesToDestroy )
            {
                items.Remove( placeable );
            }
            
            Console.WriteLine($"Optimized away {placeablesToDestroy.Count} sand! Remaining total is {beforeOptimizationCount}. Would've been {_sandSpawn.currentSandId} + {RockCount}.");
        }

        public static NextMove GetNextMove(Sand sand)
        {
            bool isAreaBelowEmpty = false;
            bool isAreaBelowLeftEmpty = false;
            bool isAreaBelowRightEmpty = false;
            bool hasSandReachedFloor = false;

            Parallel.Invoke( () => isAreaBelowEmpty = IsAreaBelowEmpty( sand.CoordinatesRow, sand.CoordinatesColumn ),
                            () => isAreaBelowLeftEmpty = IsAreaBelowLeftEmpty( sand.CoordinatesRow, sand.CoordinatesColumn ),
                            () => isAreaBelowRightEmpty = IsAreaBelowRightEmpty( sand.CoordinatesRow, sand.CoordinatesColumn ),
                            () => hasSandReachedFloor = HasSandReachedFloorY(sand));

            return new NextMove( isAreaBelowEmpty, isAreaBelowRightEmpty, isAreaBelowLeftEmpty, hasSandReachedFloor );
        }

        private static bool HasSandReachedFloorY( Sand sand )
        {
            return sand.CoordinatesRow == floorY;
        }
    }
}