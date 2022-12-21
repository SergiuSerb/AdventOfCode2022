using System.Collections.Generic;

namespace Day17.Models.Rocks
{
    //.#.
    //###
    //.#.
    public class PlusRock : Rock
    {
        public PlusRock(int id)
        {
            Id = id;
            components = new List<RockComponent>()
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