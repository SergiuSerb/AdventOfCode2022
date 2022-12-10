namespace Day9.Models
{
    public class PositionHistoryEntry
    {
        public int Id { get; }
        
        public Point Position { get; }

        public PositionHistoryEntry(int id, Point position)
        {
            Id = id;
            Position = position;
        }
    }
}