using Day17.Tools.Math;

namespace Day17.Models
{
    public class RockComponent : IPlaceable
    {
        public int CoordinateRow { get; }

        public int CoordinateColumn { get; }

        public RockComponent(int coordinateRow, int coordinateColumn)
        {
            CoordinateRow = coordinateRow;
            CoordinateColumn = coordinateColumn;
        }
    }
}