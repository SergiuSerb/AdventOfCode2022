namespace Day9.Models
{
    public class Point
    {
        public string Id => $"{X}x{Y}";
        
        public int X { get; }

        public int Y { get; }
        
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}