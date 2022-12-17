using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Day14.Models;

namespace Day14
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] inputLines = ReadInputFile();
            Map map = CreateMap(inputLines);

            Simulator simulator = new Simulator(map);
            
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
            finally
            {
                Console.Read();
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
                            if ( !Map.Items.Any(x => x.CoordinatesRow == rowIndex && x.CoordinatesColumn == columnIndex) )
                            {
                                Map.Items.Add(new Rock(currentRockIndex, rowIndex, columnIndex));
                                currentRockIndex++;
                            }

                        }
                    }
                    
                    for (int rowIndex = endRow; rowIndex <= startRow; rowIndex++)
                    {
                        for (int columnIndex = startColumn; columnIndex <= endColumn; columnIndex++)
                        {
                            if ( !Map.Items.Any(x => x.CoordinatesRow == rowIndex && x.CoordinatesColumn == columnIndex) )
                            {
                                Map.Items.Add(new Rock(currentRockIndex, rowIndex, columnIndex));
                                currentRockIndex++;
                            }
                        }
                    }
                    
                    for (int rowIndex = startRow; rowIndex <= endRow; rowIndex++)
                    {
                        for (int columnIndex = endColumn; columnIndex <= startColumn; columnIndex++)
                        {
                            if ( !Map.Items.Any(x => x.CoordinatesRow == rowIndex && x.CoordinatesColumn == columnIndex) )
                            {
                                Map.Items.Add(new Rock(currentRockIndex, rowIndex, columnIndex));
                                currentRockIndex++;
                            }
                        }
                    }
                    
                    for (int rowIndex = endRow; rowIndex <= startRow; rowIndex++)
                    {
                        for (int columnIndex = endColumn; columnIndex <= startColumn; columnIndex++)
                        {
                            if ( !Map.Items.Any(x => x.CoordinatesRow == rowIndex && x.CoordinatesColumn == columnIndex) )
                            {
                                Map.Items.Add(new Rock(currentRockIndex, rowIndex, columnIndex));
                                currentRockIndex++;
                            }
                        }
                    }

                    currentGroupIndex++;
                }
            }
            
            Map.FloorY = Map.Items.Max(x => x.CoordinatesRow) + 1;
            Map.RockCount = currentRockIndex;

            return map;
        }

        private static string[] ReadInputFile()
        {
            string? executingPath = Path.GetDirectoryName( Assembly.GetEntryAssembly()?.Location );
            const string inputFileName = "input.txt";
            string inputPath = $"{executingPath}\\{inputFileName}";

            ReadLines( inputPath, out string[] strings );
    
            return strings;
        }

        private static void ReadLines( string s, out string[] inputLines )
        {
            try
            {
                inputLines = File.ReadAllLines( s );
            }
            catch (Exception)
            {
                throw new ArgumentNullException( "No lines could be read from input file." );
            }
        }

        private static IList<string> MatchRegex( string stringToMatch, string regexPattern)
        {
            Regex regex = new Regex( regexPattern );
            Match match = regex.Match( stringToMatch );

            IList<string> groups = new List<string>();
            
            foreach ( Group group in match.Groups) 
            {
                groups.Add(group.Value);
            }

            groups.RemoveAt(0);
            return groups;
        }
    }
}