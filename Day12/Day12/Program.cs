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
        }

        private static Pathfinder CreatePathFinder( List<string> inputLines )
        {
            IList<IList<Tile>> tiles = new List<IList<Tile>>();
            int currentTileId = 0;
            MapSettings.width = inputLines.First().Length;
            
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