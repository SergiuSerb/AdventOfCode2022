namespace Day14.Models
{
    public class SandSpawn : IPlaceable
    {
        public int CoordinatesRow { get; set; }

        public int CoordinatesColumn { get; set; }

        public int currentSandId;
        private bool _killYReached;
        private bool _sandSpawnReached;

        public SandSpawn(int coordinatesRow, int coordinatesColumn)
        {
            CoordinatesRow = coordinatesRow;
            CoordinatesColumn = coordinatesColumn;
            currentSandId = 1;
        }

        public void Spawn()
        {
            Map.Print();
            Sand sand = new Sand(currentSandId, this);
            currentSandId += 1;
            Map.Items.Add(sand);
            
            sand.MoveTo(CoordinatesRow, CoordinatesColumn);
        }

        public void SpawnedSandIsStable()
        {
            if (currentSandId <= 21)
            {
                if (_killYReached)
                {
                    return;
                }
                
                if (_sandSpawnReached)
                {
                    return;
                }

                if ( currentSandId % 100 == 0 )
                {
                    Map.DestructNonSignificantSand();
                }
                
                Spawn();
            }
        }

        public void KillYReached()
        {
            _killYReached = true;
        }

        public void SandSpawnReached()
        {
            _sandSpawnReached = true;
        }
    }
}