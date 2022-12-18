using Day15.Tools.Math;

namespace Day15.Models
{
    public class Sensor : IPlaceable
    {
        public int Id { get; }
        
        public int CoordinateRow { get; }

        public int CoordinateColumn { get; }

        public Beacon ClosestBeacon { get; set; }
        
        public int DistanceToBeacon { get; private set; }

        public Sensor( int id, int coordinateRow, int coordinateColumn )
        {
            Id = id;
            CoordinateRow = coordinateRow;
            CoordinateColumn = coordinateColumn;
        }

        public void LinkBeacon( Beacon beacon )
        {
            ClosestBeacon = beacon;
            DistanceToBeacon = MathHelper.ManhattanDistance( this, beacon );
        }
    }
}