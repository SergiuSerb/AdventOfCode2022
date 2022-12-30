using Day17.Tools.Math;

namespace Day17.Models
{
    public class RockComponent : IPlaceable
    {
        public int CoordinateRow { get; set;}

        public int CoordinateColumn { get; set;}

        public RockComponent(int coordinateRow, int coordinateColumn)
        {
            CoordinateRow = coordinateRow;
            CoordinateColumn = coordinateColumn;
        }

        public RockComponent( RockComponent rockComponent ) : this(rockComponent.CoordinateRow, rockComponent.CoordinateColumn)
        {
        }
    }
}