using System;
using System.Collections.Generic;
using System.Linq;
using Day16.Models;
using Day16.Tools.FileReader;

namespace Day16
{
    internal static class Program
    {
        private const string _inputFileName = "input2.txt";
        private const string _regexFormat = "Valve (.*) has flow rate=(.*); tunnels lead to valves (.*)";
        
        private static void Main()
        {
            string[] inputLines = FileReader.ReadInputFile( _inputFileName );
            IList<Room> rooms = CreateRooms( inputLines );
        }

        private static IList<Room> CreateRooms( string[] inputLines )
        {
            IList<Room> rooms = new List<Room>();
            
            foreach ( string inputLine in inputLines )
            {
                IList<string> dividedInputLine = RegexGroupMatcher.MatchRegex( inputLine, _regexFormat );

                Valve valve = new Valve( dividedInputLine[0], int.Parse( dividedInputLine[1] ) );

                if ( rooms.All( x => x.Id != valve.Id ) )
                {
                    Room
                }
            }
        }
    }
}