using System;
using System.Linq;

namespace Day8
{
    public class Forest
    {
        private readonly Tree[,] _trees;

        public Forest(Tree[,] trees)
        {
            _trees = trees ?? throw new ArgumentNullException(nameof(trees));
        }

        public int GetNumberOfVisibleTrees()
        {
            return _trees.Cast<Tree>().Count(tree => tree.IsVisible);
        }

        public void Print()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            for (int rowIndex = 0; rowIndex < _trees.GetLength(0); rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < _trees.GetLength(1); columnIndex++)
                {
                    Console.BackgroundColor = _trees[rowIndex, columnIndex].IsVisible ? ConsoleColor.Green : ConsoleColor.Red;
                    Console.Write(_trees[rowIndex, columnIndex].Height);
                }

                Console.WriteLine();
            }
        }

        public Tree GetMaxScenicScoreTree()
        {
            int maxScenicScore = _trees.Cast<Tree>().Max(x => x.ScenicScore);

            return _trees.Cast<Tree>().First(x => x.ScenicScore == maxScenicScore);
        }
    }
}