namespace Day17.Tools.Math
{
    public class CustomRange
    {
        public int Minimum { get; }

        public int Maximum { get; }

        public CustomRange( int minimum, int maximum )
        {
            if ( minimum > maximum )
            {
                Minimum = maximum;
                Maximum = minimum;
            }
            else
            {
                Minimum = minimum;
                Maximum = maximum;
            }
        }

        public bool Includes( int x )
        {
            return Minimum <= x && x <= Maximum;
        }
        
        public bool Includes( CustomRange range)
        {
            return Minimum <= range.Minimum && range.Maximum <= Maximum;
        }
        
        public bool Intersects( CustomRange range)
        {
            return Minimum <= range.Minimum && range.Minimum <= Maximum ||
                   Minimum <= range.Maximum && range.Maximum <= Maximum;
        }
    }
}