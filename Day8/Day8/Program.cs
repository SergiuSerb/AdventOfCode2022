using System;
using System.IO;
using System.Reflection;

namespace Day8
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] inputLines = ReadInputFile();
            Forest forest = CreateForest(inputLines);
            DetermineNumberOfVisibleTrees(forest);
            DetermineMaxScenicScore(forest);
            forest.Print();
        }

        private static void DetermineMaxScenicScore(Forest forest)
        {
            Tree maxScenicScoreTree = forest.GetMaxScenicScoreTree();
            Console.WriteLine($"The tree with the highest scenic score is {maxScenicScoreTree.Id}. with a scenic score of {maxScenicScoreTree.ScenicScore}.");
        }

        private static void DetermineNumberOfVisibleTrees(Forest forest)
        {
            int visibleTreesCount = forest.GetNumberOfVisibleTrees();
            Console.WriteLine($"The number of visible trees is {visibleTreesCount}.");
        }

        private static Forest CreateForest(string[] inputLines)
        {
            Tree[,] trees = new Tree[inputLines[0].Length, inputLines.Length];

            for (int rowIndex = 0; rowIndex < inputLines.Length; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < inputLines[0].Length; columnIndex++)
                {
                    Tree tree = new Tree(rowIndex * inputLines[0].Length + columnIndex, int.Parse(inputLines[rowIndex][columnIndex].ToString()));

                    trees[rowIndex, columnIndex] = tree;
                }
            }
            
            for (int rowIndex = 0; rowIndex < trees.GetLength(0); rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < trees.GetLength(1); columnIndex++)
                {
                    Tree northNeighbour = rowIndex - 1 >= 0 ? trees[rowIndex - 1, columnIndex] : null;
                    Tree southNeighbour = rowIndex + 1 < trees.GetLength(0) ? trees[rowIndex + 1, columnIndex] : null;
                    Tree eastNeighbour = columnIndex + 1 < trees.GetLength(1) ? trees[rowIndex, columnIndex + 1] : null;
                    Tree westNeighbour = columnIndex - 1 >= 0 ? trees[rowIndex, columnIndex - 1] : null;
                    
                    trees[rowIndex, columnIndex].SetNeighbours(northNeighbour, southNeighbour, westNeighbour, eastNeighbour);
                }
            }

            return new Forest(trees);
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