using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Day12.Models;

namespace Day12
{
    internal static class Program
    {
        private static void Main( string[] args )
        {
            List<string> inputLines = ReadInputFile().ToList();
            Pathfinder pathfinder = CreatePathFinder( inputLines );
            
            DetermineShortestPathLengthFromGivenStartPoint(pathfinder);
            DetermineShortestPathLengthOverall(pathfinder);
        }

        private static void DetermineShortestPathLengthOverall(Pathfinder pathfinder)
        {
            int shortestPath = int.MaxValue;
            Tile target = pathfinder.Tiles.First( x => x.Any( x => x.TileType == TileType.Target ) )
                .First( x => x.TileType == TileType.Target );

            IList<Tile> possibleStartingTiles = pathfinder.Tiles.SelectMany(x => x.Select(x => x).Where(x => x.Height == 'a')).ToList();
            
            foreach (Tile tile in possibleStartingTiles)
            {
                var path = pathfinder.FindPath(tile, target);

                shortestPath = path.Count > 1 ? Math.Min(shortestPath, path.Count) : shortestPath ;
            }
            
            Console.WriteLine();
            Console.WriteLine($"The shortest path to the destination is {shortestPath}.");
        }

        private static void DetermineShortestPathLengthFromGivenStartPoint(Pathfinder pathfinder)
        {
            Tile source = pathfinder.Tiles.First( x => x.Any( x => x.TileType == TileType.Source ) )
                .First( x => x.TileType == TileType.Source );
            Tile target = pathfinder.Tiles.First( x => x.Any( x => x.TileType == TileType.Target ) )
                .First( x => x.TileType == TileType.Target );
            IList<Tile> path = pathfinder.FindPath(source, target);
            
            Console.WriteLine();
            Console.WriteLine($"The shortest path to the destination is {path.Count}.");
        }

        private static Pathfinder CreatePathFinder( List<string> inputLines )
        {
            IList<IList<Tile>> tiles = new List<IList<Tile>>();
            int currentTileId = 0;
            MapSettings.width = inputLines.First().Length;
            MapSettings.height = inputLines.Count;
            
            foreach ( string inputLine in inputLines )
            {
                IList<Tile> row = new List<Tile>();

                foreach ( char tile in inputLine )
                {
                    if ( char.IsLower(tile) )
                    {
                        row.Add(new Tile(currentTileId++, tile, TileType.Intermediary));
                        continue;
                    }

                    if ( tile == 'S' )
                    {
                        row.Add(new Tile(currentTileId++, 'a', TileType.Source));
                    }
                    else
                    {
                        row.Add(new Tile(currentTileId++, 'z', TileType.Target));
                    }
                }
                
                tiles.Add(row);
            }

            return new Pathfinder( tiles );
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