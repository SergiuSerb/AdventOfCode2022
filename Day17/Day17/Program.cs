using System;
using System.Collections.Generic;
using Day17.Models;
using Day17.Models.Moves;
using Day17.Tools.FileReader;

namespace Day17
{
    internal static class Program
    {
        private const string _inputFileName = "input2.txt";

        private static void Main()
        {
            string[] inputLines = FileReader.ReadInputFile( _inputFileName );
            List<IMove> moves = CreateMoves( inputLines );
            MoveContainer moveContainer = new MoveContainer(moves);
            DetermineTopHeight(moveContainer, 2022);
            DetermineTopHeight(moveContainer, 1000000000000);
        }

        private static void DetermineTopHeight(MoveContainer moveContainer, ulong numberOfRocks)
        {
            PredictiveSimulator predictiveSimulator = new PredictiveSimulator();
            ulong topHeight = predictiveSimulator.Run(moveContainer, numberOfRocks);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"After {numberOfRocks} rounds, the top height is {topHeight}.");
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static List<IMove> CreateMoves( string[] inputLines )
        {
            List<IMove> moves = new List<IMove>();

            foreach ( char moveRepresentation in inputLines[0] )
            {
                IMove move = moveRepresentation == '>' ? new Right() : new Left();
                moves.Add(move);
                moves.Add(new Down());
            }

            return moves;
        }
    }
}