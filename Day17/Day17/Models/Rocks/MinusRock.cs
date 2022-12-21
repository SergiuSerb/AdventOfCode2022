using System.Collections.Generic;
using Day17.Tools.Math;

namespace Day17.Models.Rocks
{
    public class MinusRock : Rock, IPlaceable
    {
        //####
        public int Id { get; }
        
        public int CoordinateRow { get; set;}

        public int CoordinateColumn { get; set;}
        
        
        public int RowOffset => 0;
        
        public int ColumnOffset => 0;

        private IList<RockComponent> _components = new List<RockComponent>()
        {
            new RockComponent(0, 0),
            new RockComponent(0, 1),
            new RockComponent(0, 2),
            new RockComponent(0, 3)
        };

        public MinusRock(int id)
        {
            Id = id;
        }
    }
}