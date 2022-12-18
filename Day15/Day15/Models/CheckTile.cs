namespace Day15.Models
{
    public class CheckTile : IPlaceable
    {
        public int CoordinateRow { get; private set; }

        public int CoordinateColumn { get; private set; }

        public CheckTile( int coordinateRow, int coordinateColumn )
        {
            CoordinateRow = coordinateRow;
            CoordinateColumn = coordinateColumn;
        }

        public void MoveTo( int coordinateRow, int coordinateColumn )
        {
            CoordinateRow = coordinateRow;
            CoordinateColumn = coordinateColumn;
        }
    }
}