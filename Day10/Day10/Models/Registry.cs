namespace Day10.Models
{
    internal class Registry
    {
        public int X { get; set; }

        public Registry( int x )
        {
            X = x;
        }

        public Registry( Registry registry )
        {
            X = registry.X;
        }
    }
}