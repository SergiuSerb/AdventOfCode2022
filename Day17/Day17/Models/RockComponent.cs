using Day17.Tools.Math;

namespace Day17.Models
{
    public class RockComponent : IPlaceable
    {
        public long CoordinateRow { get; set;}

        public long CoordinateColumn { get; set;}

        public RockComponent(long coordinateRow, long coordinateColumn)
        {
            CoordinateRow = coordinateRow;
            CoordinateColumn = coordinateColumn;
        }

        public RockComponent( RockComponent rockComponent ) : this(rockComponent.CoordinateRow, rockComponent.CoordinateColumn)
        {
        }
    }
}