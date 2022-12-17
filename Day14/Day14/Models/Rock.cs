namespace Day14.Models
{
    public class Rock : IPlaceable
    {
        public int Id { get; }
        
        public int CoordinatesRow { get; set; }

        public int CoordinatesColumn { get; set; }

        public Rock(int id, int coordinatesRow, int coordinatesColumn)
        {
            Id = id;
            CoordinatesRow = coordinatesRow;
            CoordinatesColumn = coordinatesColumn;
        }
    }
}