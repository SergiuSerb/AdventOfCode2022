using System.Collections.Generic;

namespace Day17.Models.Rocks
{
    public class MinusRock : Rock
    {
        //####
        public MinusRock(ulong id)
        {
            Id = id;
            CoordinateColumn = 3;
            Components = new List<RockComponent>()
            {
                new RockComponent(0, 0),
                new RockComponent(0, 1),
                new RockComponent(0, 2),
                new RockComponent(0, 3)
            };
        }
    }
}