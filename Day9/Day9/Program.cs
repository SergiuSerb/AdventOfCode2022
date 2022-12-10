using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Day9.Models;

namespace Day9
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] inputLines = ReadInputFile();
            
            // Rope rope = PerformMoves2Rope(inputLines);
            // DetermineUniqueTailPositions(rope);
            
            Rope rope2 = PerformMoves10Rope(inputLines);
            DetermineUniqueTailPositions(rope2);
        }

        private static void DetermineUniqueTailPositions(Rope rope)
        {
            IList<Point> uniqueTailPositions = rope.GetUniqueTailPositions();
            Console.WriteLine($"The tail of the {rope.RopePointsCount} sections long rope has touched {uniqueTailPositions.Count} positions.");
        }

        private static Rope PerformMoves10Rope(string[] inputLines)
        {
            Rope rope = new Rope(10);
            PerformMovesFromInput(inputLines, rope);

            return rope;
        }
        
        private static Rope PerformMoves2Rope(string[] inputLines)
        {
            Rope rope = new Rope(2);
            PerformMovesFromInput(inputLines, rope);

            return rope;
        }

        private static void PerformMovesFromInput(string[] inputLines, Rope rope)
        {
            int moveIndex = 1;
            const char separator = ' ';
            foreach (string inputLine in inputLines)
            {
                string[] splitLine = inputLine.Split(separator);
                int numberOfRepetitions = int.Parse(splitLine[1]);

                for (int repetitionIndex = 1; repetitionIndex <= numberOfRepetitions; repetitionIndex++)
                {
                    switch (splitLine[0])
                    {
                        case "U":
                            rope.MoveDown(moveIndex++);
                            break;
                        case "D":
                            rope.MoveUp(moveIndex++);
                            break;
                        case "L":
                            rope.MoveLeft(moveIndex++);
                            break;
                        case "R":
                            rope.MoveRight(moveIndex++);
                            break;
                    }
                }
            }
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
    }
}