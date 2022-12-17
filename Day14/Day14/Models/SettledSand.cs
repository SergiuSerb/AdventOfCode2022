namespace Day14.Models
{
    public class SettledSand : IPlaceable
    {
        public int Id { get; }
        
        public int CoordinatesRow { get; set; }

        public int CoordinatesColumn { get; set; }

        public SettledSand( int id, int coordinatesRow, int coordinatesColumn )
        {
            Id = id;
            CoordinatesRow = coordinatesRow;
            CoordinatesColumn = coordinatesColumn;
        }
    }
}