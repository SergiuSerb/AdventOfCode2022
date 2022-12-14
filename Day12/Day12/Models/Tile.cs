namespace Day12.Models
{
    internal class Tile
    {
        public int Id { get; }

        public char Height { get; }
        
        public int CoordinatesRow { get; }
        
        public int CoordinatesColumn { get; }
        
        public TileType TileType { get; }

        public int CostFromSource { get; set; }

        public int DistanceToTarget { get; set; }

        public int Fitness => CostFromSource + DistanceToTarget;
        
        public Tile Neighbour { get; set; }

        public Tile( int id, char height, TileType tileType )
        {
            Id = id;
            Height = height;
            TileType = tileType;
            CoordinatesRow = id / MapSettings.width;
            CoordinatesColumn = id % MapSettings.width;
        }
    }
}