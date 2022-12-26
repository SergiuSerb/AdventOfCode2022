using System.Collections.Generic;

namespace Day17.Models.Rocks
{
    public class SquareRock : Rock
    {
        //##
        //##
        public SquareRock(ulong id)
        {
            Id = id;
            CoordinateColumn = 3;
            Components = new List<RockComponent>()
            {
                new RockComponent(0, 0),
                new RockComponent(0, 1),
                new RockComponent(1, 0),
                new RockComponent(1, 1)
            };
        }
    }
}