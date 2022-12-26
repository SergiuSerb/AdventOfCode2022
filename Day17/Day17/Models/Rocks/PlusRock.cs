using System.Collections.Generic;

namespace Day17.Models.Rocks
{
    //.#.
    //###
    //.#.
    public class PlusRock : Rock
    {
        public PlusRock(ulong id)
        {
            Id = id;
            CoordinateColumn = 4;
            Components = new List<RockComponent>()
            {
                new RockComponent(0, 0),
                new RockComponent(1, 0),
                new RockComponent(2, 0),
                new RockComponent(1, -1),
                new RockComponent(1, 1)
            };
        }
    }
}