using System.Collections.Generic;
using Day17.Tools.Math;

namespace Day17.Models.Rocks
{
    public class LineRock : Rock, IPlaceable
    {
        //#
        //#
        //#
        //#
        public int Id { get; }
        
        public int CoordinateRow { get; set;}

        public int CoordinateColumn { get; set;}
        
        public int RowOffset => 0;
        
        public int ColumnOffset => 0;

        private IList<RockComponent> _components = new List<RockComponent>()
        {
            new RockComponent(0, 0),
            new RockComponent(1, 0),
            new RockComponent(2, 0),
            new RockComponent(3, 0)
        };

        public LineRock(int id)
        {
            Id = id;
        }
    }
}