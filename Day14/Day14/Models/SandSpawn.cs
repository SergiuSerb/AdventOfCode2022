namespace Day14.Models
{
    public class SandSpawn : IPlaceable
    {
        public int CoordinatesRow { get; set; }

        public int CoordinatesColumn { get; set; }

        public  int CurrentSandId;
        private bool _killYReached;

        public SandSpawn(int coordinatesRow, int coordinatesColumn)
        {
            CoordinatesRow = coordinatesRow;
            CoordinatesColumn = coordinatesColumn;
            CurrentSandId = 1;
        }

        public void Spawn()
        {
            Sand sand = new Sand(CurrentSandId, this);
            CurrentSandId += 1;
            Map.Items.Add(sand);
            
            sand.MoveTo(CoordinatesRow, CoordinatesColumn);
        }

        public void SpawnedSandIsStable()
        {
            if (CurrentSandId <= 1000)
            {
                if (_killYReached)
                {
                    return;
                }
                
                Spawn();
            }
        }

        public void KillYReached()
        {
            _killYReached = true;
        }
    }
}