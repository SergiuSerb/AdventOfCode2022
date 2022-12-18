namespace Day15.Models
{
    public class Beacon : IPlaceable
    {
        public int Id { get; }
        
        public int CoordinateRow { get;  }

        public int CoordinateColumn { get;  }

        public Beacon( int id, int coordinateRow, int coordinateColumn )
        {
            Id = id;
            CoordinateRow = coordinateRow;
            CoordinateColumn = coordinateColumn;
        }
    }
}