using System.Collections.Generic;
using Day17.Tools.Math;

namespace Day17.Models.Rocks
{
    public class SquareRock : IPlaceable
    {
        //##
        //##
        public int Id { get; }
        
        public int CoordinateRow { get; }

        public int CoordinateColumn { get; }

        private IList<RockComponent> _components = new List<RockComponent>()
        {
            new RockComponent(0, 0),
            new RockComponent(0, 1),
            new RockComponent(1, 0),
            new RockComponent(1, 1)
        };

        public SquareRock(int id)
        {
            Id = id;
        }
    }
}