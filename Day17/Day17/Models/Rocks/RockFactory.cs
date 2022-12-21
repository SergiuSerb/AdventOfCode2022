using System;
using System.Collections.Generic;

namespace Day17.Models.Rocks
{
    public static class RockFactory
    {
        private static readonly IList<Type> _rockTypes = new List<Type>()
        {
            typeof(MinusRock), typeof(PlusRock), typeof(CornerRock), typeof(LineRock), typeof(SquareRock),
        };

        private static int _currentRockType = -1;
        
        public static Rock Create(int rockId)
        {
            _currentRockType++;
            return (Rock)Activator.CreateInstance( _rockTypes[_currentRockType % _rockTypes.Count ], rockId );
        }
    }
}