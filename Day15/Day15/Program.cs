using System;
using System.Collections.Generic;
using System.Numerics;
using Day15.Models;
using Day15.Tools.FileReader;

namespace Day15
{
    internal static class Program
    {
        private const string _inputFileName = "input.txt";
        private const string _regexFormat = "Sensor at x=(.*), y=(.*): closest beacon is at x=(.*), y=(.*)";
        
        private static void Main( string[] args )
        {
            string[] inputLines = FileReader.ReadInputFile( _inputFileName );
            Map map = CreateMap( inputLines );
            DetermineScannedTilesAtLine( 2000000, map );
            DetermineUnscannedTileInBoundingBox( 0, 4000000, map );
        }

        private static void DetermineUnscannedTileInBoundingBox( int minRowColumn, int maxRowColumn, Map map )
        {
            BigInteger unscannedTileFrequency = map.FindUnscannedTilesInBoundingBox( minRowColumn, maxRowColumn );
            Console.WriteLine($"The unscanned tile frequency is {unscannedTileFrequency}.");
        }

        private static void DetermineScannedTilesAtLine( int lineIndex, Map map )
        {
            int scannedTiles = map.FindScannedTilesAtLine( lineIndex );
            Console.WriteLine($"The total number of scanned tiles on row {lineIndex} is {scannedTiles}.");
        }

        private static Map CreateMap( string[] inputLines )
        {
            Map map = new Map();
            
            int currentBeaconId = 1;
            int currentSensorId = 1;
            
            foreach ( string inputLine in inputLines )
            {
                IList<string> groups = RegexGroupMatcher.MatchRegex( inputLine, _regexFormat );

                Beacon beacon = new Beacon( currentBeaconId++, int.Parse( groups[3] ), int.Parse( groups[2] ) );
                Sensor sensor = new Sensor( currentSensorId++, int.Parse( groups[1] ), int.Parse( groups[0] ) );
                
                sensor.LinkBeacon(beacon);
                
                map.AddItem(sensor);
                map.AddItem( beacon );
            }

            return map;
        }
    }
}