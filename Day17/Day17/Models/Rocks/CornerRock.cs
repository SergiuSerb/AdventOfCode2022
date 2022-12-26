using System.Collections.Generic;

namespace Day17.Models.Rocks
{
    //..#
    //..#
    //###
    public class CornerRock : Rock
    {
        public CornerRock(ulong id)
        {
            Id = id;
            Components = new List<RockComponent>()
            {
                new RockComponent(0, 0),
                new RockComponent(1, 0),
                new RockComponent(2, 0),
                new RockComponent(0, -1),
                new RockComponent(0, -2)
            };
            CoordinateColumn = 5;
        }
    }
}