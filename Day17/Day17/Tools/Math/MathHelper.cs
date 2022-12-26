namespace Day17.Tools.Math
{
    public static class MathHelper
    {
        public static long ManhattanDistance( IPlaceable left, IPlaceable right )
        {
            return System.Math.Abs( left.CoordinateRow - right.CoordinateRow ) +
                   System.Math.Abs( left.CoordinateColumn - right.CoordinateColumn );
        }
    }
}