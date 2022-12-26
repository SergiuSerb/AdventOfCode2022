namespace Day17.Tools.Math
{
    public class CustomRange
    {
        public long Minimum { get; }

        public long Maximum { get; }

        public CustomRange( long minimum, long maximum )
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

        public bool Includes( long x )
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