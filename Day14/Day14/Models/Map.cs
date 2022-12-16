using System;
using System.Collections.Generic;
using System.Linq;

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
        
        public static bool IsAreaAboveLeftEmpty(int coordinatesRow, int coordinatesColumn)
        {
            return !Items.Any(x => x.CoordinatesRow == coordinatesRow - 1 && x.CoordinatesColumn == coordinatesColumn - 1);
        }
        
        public static bool IsAreaAboveRightEmpty(int coordinatesRow, int coordinatesColumn)
        {
            return !Items.Any(x => x.CoordinatesRow == coordinatesRow - 1 && x.CoordinatesColumn == coordinatesColumn + 1);
        }
        
        public static bool IsAreaBelowRightEmpty(int coordinatesRow, int coordinatesColumn)
        {
            return !Items.Any(x => x.CoordinatesRow == coordinatesRow + 1 && x.CoordinatesColumn == coordinatesColumn + 1);
        }
        
        public static bool IsAreaAboveEmpty(int coordinatesRow, int coordinatesColumn)
        {
            return !Items.Any(x => x.CoordinatesRow == coordinatesRow - 1 && x.CoordinatesColumn == coordinatesColumn);
        }        
        
        public static bool IsAreaRightEmpty(int coordinatesRow, int coordinatesColumn)
        {
            return !Items.Any(x => x.CoordinatesRow == coordinatesRow && x.CoordinatesColumn == coordinatesColumn + 1);
        }        
        
        public static bool IsAreaLeftEmpty(int coordinatesRow, int coordinatesColumn)
        {
            return !Items.Any(x => x.CoordinatesRow == coordinatesRow && x.CoordinatesColumn == coordinatesColumn - 1);
        }

        private static bool CanBeDeleted( Sand sand )
        {
            return !IsAreaAboveEmpty( sand.CoordinatesRow, sand.CoordinatesColumn ) &&
                   !IsAreaBelowEmpty( sand.CoordinatesRow, sand.CoordinatesColumn ) &&
                   !IsAreaLeftEmpty( sand.CoordinatesRow, sand.CoordinatesColumn ) &&
                   !IsAreaRightEmpty( sand.CoordinatesRow, sand.CoordinatesColumn ) &&
                   !IsAreaAboveLeftEmpty( sand.CoordinatesRow, sand.CoordinatesColumn ) &&
                   !IsAreaAboveRightEmpty( sand.CoordinatesRow, sand.CoordinatesColumn );
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
                if (placeable is Sand)
                {
                    visualRepresentation[placeable.CoordinatesRow - rowOffset, placeable.CoordinatesColumn - columnOffset] = 'o';
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
            IList<Sand> sandToDestroy = Items.OfType<Sand>().Where( CanBeDeleted ).ToList();
            
            foreach ( Sand sand in sandToDestroy )
            {
                Items.Remove( sand );
            }
            
            Console.WriteLine($"Optimized away {sandToDestroy.Count} sand!");
        }

        public static bool CanMoveDown(Sand sand)
        {
            return (IsAreaBelowRightEmpty(sand.CoordinatesRow, sand.CoordinatesColumn) ||
                    IsAreaBelowLeftEmpty(sand.CoordinatesRow, sand.CoordinatesColumn) ||
                    IsAreaBelowEmpty(sand.CoordinatesRow, sand.CoordinatesColumn)) &&
                   !HasSandReachedFloorY(sand);
        }
        
        public static bool HasSandReachedFloorY( Sand sand )
        {
            return sand.CoordinatesRow == FloorY;
        }
        
        public static bool HasSandReachedSandSpawn( Sand sand )
        {
            return sand.CoordinatesRow == SandSpawn.CoordinatesRow && sand.CoordinatesColumn == SandSpawn.CoordinatesColumn;
        }
    }
}