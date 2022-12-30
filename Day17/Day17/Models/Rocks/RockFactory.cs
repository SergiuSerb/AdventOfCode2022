using System;
using System.Collections.Generic;

namespace Day17.Models.Rocks
{
    public class RockFactory
    {
        private static readonly IList<Type> _rockTypes = new List<Type>()
        {
            typeof(MinusRock), typeof(PlusRock), typeof(CornerRock), typeof(LineRock), typeof(SquareRock)
        };

        private readonly Dictionary<Type, int> rockSpawnStats = new Dictionary<Type, int>()
        {
            {typeof(MinusRock), 0},
            {typeof(PlusRock), 0},
            {typeof(CornerRock), 0},
            {typeof(LineRock), 0},
            {typeof(SquareRock), 0}
        };

        private int _currentRockType = -1;

        public static readonly int RockTypesCount = _rockTypes.Count;
        
        public Rock Create(ulong rockId)
        {
            _currentRockType++;
            rockSpawnStats[_rockTypes[_currentRockType % _rockTypes.Count]]++;
            return (Rock)Activator.CreateInstance( _rockTypes[_currentRockType % _rockTypes.Count ], rockId );
        }

        public Type GetLastSpawnedRockType()
        {
            return _rockTypes[_currentRockType % _rockTypes.Count];
        }
    }
}