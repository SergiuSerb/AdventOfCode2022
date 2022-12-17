using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day14.Models
{
    public class Map
    {
        public static IList<IPlaceable> Items;

        public static int FloorY = 0;

        public static SandSpawn SandSpawn;

        public Map()
        {
            Items = new List<IPlaceable>();
        }

        public static int RockCount { get; set; }

        public void AddSandSpawn(SandSpawn sandSpawn)
        {
            Items.Add(sandSpawn);
            SandSpawn = sandSpawn;
        }

        public static bool IsAreaBelowEmpty(int coordinatesRow, int coordinatesColumn)
        {
            return !Items.Any(x => x.CoordinatesRow == coordinatesRow + 1 && x.CoordinatesColumn == coordinatesColumn);
        }

        public static bool IsAreaBelowLeftEmpty(int coordinatesRow, int coordinatesColumn)
        {
            return !Items.Any(x => x.CoordinatesRow == coordinatesRow + 1 && x.CoordinatesColumn == coordinatesColumn - 1);
        }

        private static bool IsAreaAboveLeftEmpty(int coordinatesRow, int coordinatesColumn)
        {
            return !Items.Any(x => x.CoordinatesRow == coordinatesRow - 1 && x.CoordinatesColumn == coordinatesColumn - 1);
        }

        private static bool IsAreaAboveRightEmpty(int coordinatesRow, int coordinatesColumn)
        {
            return !Items.Any(x => x.CoordinatesRow == coordinatesRow - 1 && x.CoordinatesColumn == coordinatesColumn + 1);
        }
        
        public static bool IsAreaBelowRightEmpty(int coordinatesRow, int coordinatesColumn)
        {
            return !Items.Any(x => x.CoordinatesRow == coordinatesRow + 1 && x.CoordinatesColumn == coordinatesColumn + 1);
        }

        private static bool IsAreaAboveEmpty(int coordinatesRow, int coordinatesColumn)
        {
            return !Items.Any(x => x.CoordinatesRow == coordinatesRow - 1 && x.CoordinatesColumn == coordinatesColumn);
        }

        private static bool IsAreaRightEmpty(int coordinatesRow, int coordinatesColumn)
        {
            return !Items.Any(x => x.CoordinatesRow == coordinatesRow && x.CoordinatesColumn == coordinatesColumn + 1);
        }

        private static bool IsAreaLeftEmpty(int coordinatesRow, int coordinatesColumn)
        {
            return !Items.Any(x => x.CoordinatesRow == coordinatesRow && x.CoordinatesColumn == coordinatesColumn - 1);
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
            int boundingBoxMinRow = Items.Min(x => x.CoordinatesRow);
            int boundingBoxMaxRow = Items.Max(x => x.CoordinatesRow);
            int rowOffset = boundingBoxMinRow;
            
            int boundingBoxMinColumn = Items.Min(x => x.CoordinatesColumn);
            int boundingBoxMaxColumn = Items.Max(x => x.CoordinatesColumn);
            int columnOffset = boundingBoxMinColumn;

            char[,] visualRepresentation = new char[boundingBoxMaxRow - rowOffset + 1, boundingBoxMaxColumn - columnOffset + 1];

            for (int i = 0; i < visualRepresentation.GetLength(0); i++)
            {
                for (int j = 0; j < visualRepresentation.GetLength(1); j++)
                {
                    visualRepresentation[i,j] = '.';
                }
            }

            foreach (IPlaceable placeable in Items)
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

        public static void DestructNonSignificantSand()
        {
            IList<IPlaceable> sandToDestroy = Items.Where(  CanBeDeleted ).ToList();

            int beforeOptimizationCount = Items.Count;
            
            foreach ( IPlaceable sand in sandToDestroy )
            {
                Items.Remove( sand );
            }
            
            Console.WriteLine($"Optimized away {sandToDestroy.Count} sand! Remaining total is {beforeOptimizationCount}. Would've been {SandSpawn.currentSandId} + {RockCount}.");
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
            return sand.CoordinatesRow == FloorY;
        }
    }
}