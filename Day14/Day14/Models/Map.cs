using System;
using System.Collections.Generic;
using System.Linq;

namespace Day14.Models
{
    public class Map
    {
        public static IList<IPlaceable> Items;

        public static int KillY = 0;

        public Map()
        {
            Items = new List<IPlaceable>();
        }

        public static bool IsAreaBelowEmpty(int coordinatesRow, int coordinatesColumn)
        {
            return !Items.Any(x => x.CoordinatesRow == coordinatesRow + 1 && x.CoordinatesColumn == coordinatesColumn);
        }

        public static bool IsAreaBelowLeftEmpty(int coordinatesRow, int coordinatesColumn)
        {
            return !Items.Any(x => x.CoordinatesRow == coordinatesRow + 1 && x.CoordinatesColumn == coordinatesColumn - 1);
        }
        
        public static bool IsAreaBelowRightEmpty(int coordinatesRow, int coordinatesColumn)
        {
            return !Items.Any(x => x.CoordinatesRow == coordinatesRow + 1 && x.CoordinatesColumn == coordinatesColumn + 1);
        }

        public void Print()
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
    }
}