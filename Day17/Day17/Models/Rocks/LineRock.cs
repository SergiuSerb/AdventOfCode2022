using System.Collections.Generic;

namespace Day17.Models.Rocks
{
    public class LineRock : Rock
    {
        //#
        //#
        //#
        //#
        public LineRock(ulong id)
        {
            Id = id;
            CoordinateColumn = 3;
            Components = new List<RockComponent>()
            {
                new RockComponent(0, 0),
                new RockComponent(1, 0),
                new RockComponent(2, 0),
                new RockComponent(3, 0)
            };
        }
    }
}