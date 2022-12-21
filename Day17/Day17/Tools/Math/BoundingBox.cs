namespace Day17.Tools.Math
{
    public class BoundingBox
    {
        public CustomRange RowRange { get; }

        public CustomRange ColumnRange { get; }

        public BoundingBox( CustomRange rowRange, CustomRange columnRange )
        {
            RowRange = rowRange;
            ColumnRange = columnRange;
        }

        public bool Intersects( BoundingBox boundingBox )
        {
            return RowRange.Intersects( boundingBox.RowRange ) && ColumnRange.Intersects( boundingBox.ColumnRange );
        }
    }
}