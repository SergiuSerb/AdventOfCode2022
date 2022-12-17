using System;
using System.Linq;
using Day14.Models;
using Day14.Tools.FileReader;

namespace Day14
{
    internal static class Program
    {
        private const string _inputFileName = "input.txt";

        private static void Main(string[] args)
        {
            string[] inputLines = FileReader.ReadInputFile( _inputFileName );
            Map map = CreateMap(inputLines);

            Simulator simulator = new Simulator(map, true);
            
            try
            {
                simulator.RunSimulation();
                DetermineSandCount( simulator );

                Map.Print();
                Console.Read();
            }
            catch ( Exception ex )
            {
                Console.WriteLine( ex.Message );
            }
        }

        private static void DetermineSandCount( Simulator simulator )
        {
            Console.WriteLine($"Sand spawn was reached by Sand ID {simulator.SandThatReachedSpawnId - 1}.");
        }
        
        private static Map CreateMap(string[] inputLines)
        {
            Map map = new Map();
            
            int currentRockIndex = 1;
            foreach (string inputLine in inputLines)
            {
                string[] points = inputLine.Split(" -> ");

                int currentGroupIndex = 0;
                
                while (currentGroupIndex < points.Length - 1)
                {
                    string[] startPoint = points[currentGroupIndex].Split(',');
                    string[] endPoint = points[currentGroupIndex + 1].Split(',');

                    int startRow = int.Parse(startPoint[1]);
                    int startColumn = int.Parse(startPoint[0]);                    
                    
                    int endRow = int.Parse(endPoint[1]);
                    int endColumn = int.Parse(endPoint[0]);

                    for (int rowIndex = startRow; rowIndex <= endRow; rowIndex++)
                    {
                        for (int columnIndex = startColumn; columnIndex <= endColumn; columnIndex++)
                        {
                            if ( !Map.items.Any(x => x.CoordinatesRow == rowIndex && x.CoordinatesColumn == columnIndex) )
                            {
                                Map.items.Add(new Rock(currentRockIndex, rowIndex, columnIndex));
                                currentRockIndex++;
                            }

                        }
                    }
                    
                    for (int rowIndex = endRow; rowIndex <= startRow; rowIndex++)
                    {
                        for (int columnIndex = startColumn; columnIndex <= endColumn; columnIndex++)
                        {
                            if ( !Map.items.Any(x => x.CoordinatesRow == rowIndex && x.CoordinatesColumn == columnIndex) )
                            {
                                Map.items.Add(new Rock(currentRockIndex, rowIndex, columnIndex));
                                currentRockIndex++;
                            }
                        }
                    }
                    
                    for (int rowIndex = startRow; rowIndex <= endRow; rowIndex++)
                    {
                        for (int columnIndex = endColumn; columnIndex <= startColumn; columnIndex++)
                        {
                            if ( !Map.items.Any(x => x.CoordinatesRow == rowIndex && x.CoordinatesColumn == columnIndex) )
                            {
                                Map.items.Add(new Rock(currentRockIndex, rowIndex, columnIndex));
                                currentRockIndex++;
                            }
                        }
                    }
                    
                    for (int rowIndex = endRow; rowIndex <= startRow; rowIndex++)
                    {
                        for (int columnIndex = endColumn; columnIndex <= startColumn; columnIndex++)
                        {
                            if ( !Map.items.Any(x => x.CoordinatesRow == rowIndex && x.CoordinatesColumn == columnIndex) )
                            {
                                Map.items.Add(new Rock(currentRockIndex, rowIndex, columnIndex));
                                currentRockIndex++;
                            }
                        }
                    }

                    currentGroupIndex++;
                }
            }
            
            Map.floorY = Map.items.Max(x => x.CoordinatesRow) + 1;
            Map.RockCount = currentRockIndex;

            return map;
        }
    }
}