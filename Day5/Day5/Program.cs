using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Day5.Models;

namespace Day5
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] inputLines = ReadInputFile();

            IList<CrateStack> crateStacks = CreateCrateStacks(inputLines);
            IList<Move> moves = CreateMoves(inputLines);
            PerformMovesWithCrateMover9000(moves, crateStacks);
            DetermineTopCrates(crateStacks);
            
            crateStacks = CreateCrateStacks(inputLines);
            PerformMovesWithCrateMover9001(moves, crateStacks);
            DetermineTopCrates(crateStacks);
        }

        private static void PerformMovesWithCrateMover9001(IList<Move> moves, IList<CrateStack> crateStacksForCrateMover9001)
        {
            Crane crane = new CrateMover9001(crateStacksForCrateMover9001);

            foreach (Move move in moves)
            {
                crane.Move(move);
            }
        }

        private static void PerformMovesWithCrateMover9000(IList<Move> moves, IList<CrateStack> crateStacks)
        {
            Crane crane = new CrateMover9000(crateStacks);

            foreach (Move move in moves)
            {
                crane.Move(move);
            }
        }

        private static IList<Move> CreateMoves(string[] inputLines)
        {
            int indexOfSeparator = inputLines.ToList().IndexOf("");
            List<string> initialStateInput = inputLines.TakeLast(inputLines.Count() - indexOfSeparator - 1).ToList();

            IList<Move> moves = new List<Move>();
            int moveId = 1;
            foreach (string inputLine in initialStateInput)
            {
                string[] splitLine = inputLine.Split(' ');

                int numberOfCratesToMove = int.Parse(splitLine[1]);
                int sourceStackId = int.Parse(splitLine[3]);
                int destinationStackId = int.Parse(splitLine[5]);

                Move move = new Move(moveId++, sourceStackId, destinationStackId, numberOfCratesToMove);
                moves.Add(move);
            }
            
            return moves;
        }

        private static void DetermineTopCrates(IList<CrateStack> crateStacks)
        {
            string solution = string.Empty;

            foreach (CrateStack stack in crateStacks)
            {
                Crate topCrate = stack.GetTopCrate();

                solution = $"{solution}{topCrate.Id}";
            }

            Console.WriteLine($"The crates on top are {solution}.");
        }

        private static IList<CrateStack> CreateCrateStacks(string[] inputLines)
        {
            IList<CrateStack> stacks = new List<CrateStack>();
            
            int indexOfSeparator = inputLines.ToList().IndexOf("");
            List<string> initialStateInput = inputLines.Take(indexOfSeparator).ToList();
            
            List<string> cleanedUpInputs = new List<string>();

            foreach (string inputLine in initialStateInput)
            {
                string cleanedUpInput = CleanUpInput(inputLine);
                cleanedUpInputs.Add(cleanedUpInput);
            }

            string stackIds = cleanedUpInputs.Last();
            cleanedUpInputs.Remove(stackIds);
            cleanedUpInputs.Reverse();

            for (int stackIndex = 0; stackIndex < stackIds.Length; stackIndex++)
            {
                int stackId = int.Parse(stackIds[stackIndex].ToString());
                CrateStack stack = new CrateStack(stackId);

                foreach (string cleanedUpInput in cleanedUpInputs)
                {
                    char crateId = cleanedUpInput[stackIndex];

                    if (!string.IsNullOrWhiteSpace(crateId.ToString()))
                    {
                        Crate crate = new Crate(crateId);
                        stack.Add(crate);
                    }
                }
                stacks.Add(stack);
            }
            
            return stacks;
        }

        private static string CleanUpInput(string inputLine)
        {
            string cleanedUpInput = string.Empty;
            for (int i = 1; i < inputLine.Length; i+=4)
            {
                cleanedUpInput = $"{cleanedUpInput}{inputLine[i]}";
            }
            
            return cleanedUpInput;
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