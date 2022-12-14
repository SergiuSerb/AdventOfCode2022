using System;

namespace Day13.Models
{
    internal class Sequence
    {
        public string Id { get; }
        
        public string Representation { get; }

        public Sequence(string id, string representation)
        {
            Id = id;
            Representation = representation;
        }

        public static int Compare(Sequence left, Sequence right)
        {
            int currentCharacterLeftIndex = 0;
            int currentCharacterRightIndex = 0;
            
            char? currentCharacterLeft = GetNextCharacter(left, currentCharacterLeftIndex++);
            char? currentCharacterRight = GetNextCharacter(right, currentCharacterRightIndex++);

            while (currentCharacterLeft != null && currentCharacterRight != null)
            {
                if (currentCharacterLeft == currentCharacterRight)
                {
                    currentCharacterLeft = GetNextCharacter(left, currentCharacterLeftIndex++);
                    currentCharacterRight = GetNextCharacter(right, currentCharacterRightIndex++);
                    continue;
                }
                
                if (currentCharacterLeft == '[' && currentCharacterRight != '[')
                {
                    currentCharacterLeft = GetNextCharacter(left, currentCharacterLeftIndex++);
                    continue;
                }
                
                if (currentCharacterLeft != '[' && currentCharacterRight == '[')
                {
                    currentCharacterRight = GetNextCharacter(right, currentCharacterRightIndex++);
                    continue;
                }               
                
                if (currentCharacterLeft == ']' && currentCharacterRight != ']')
                {
                    currentCharacterLeft = GetNextCharacter(left, currentCharacterLeftIndex++);
                    continue;
                }                
                
                if (currentCharacterLeft != ']' && currentCharacterRight == ']')
                {
                    currentCharacterRight = GetNextCharacter(right, currentCharacterRightIndex++);
                    continue;
                }

                int result = currentCharacterLeft < currentCharacterRight ? -1 : 1;

                PrintResult(left, right, currentCharacterRightIndex);

                return result;
            }

            if (currentCharacterLeft == null && currentCharacterRight == null)
            {
                return 0;
            }

            if (currentCharacterLeft == null && currentCharacterRight != null)
            {
                return -1;
            }
            
            return 1;
        }

        private static void PrintResult(Sequence left, Sequence right, int currentCharacterRight)
        {
            Console.WriteLine(left.Representation);
            Console.WriteLine(right.Representation);

            for (int i = 0; i < currentCharacterRight; i++)
            {
                Console.Write("-");
            }
            Console.Write("^");
            
            Console.WriteLine();
        }

        private static char? GetNextCharacter(Sequence sequence, int currentCharacterIndex)
        {
            if (currentCharacterIndex < sequence.Representation.Length )
            {
                return sequence.Representation[currentCharacterIndex];
            }

            return null;
        }
    }
}