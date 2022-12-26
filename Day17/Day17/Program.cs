using System;
using System.Collections.Generic;
using Day17.Models;
using Day17.Models.Moves;
using Day17.Tools.FileReader;

namespace Day17
{
    class Program
    {
        private const string _inputFileName = "input2.txt";
        
        static void Main(string[] args)
        {
            string[] inputLines = FileReader.ReadInputFile( _inputFileName );
            List<IMove> moves = CreateMoves( inputLines );
            MoveContainer moveContainer = new MoveContainer(moves);
            Simulator simulator = new Simulator();
            
            simulator.Run(moveContainer);
            DetermineTopHeight(simulator);
        }

        private static void DetermineTopHeight(Simulator simulator)
        {
            Console.WriteLine($"After 2022 rounds, the top height is {simulator.GetTopHeight()}.");
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