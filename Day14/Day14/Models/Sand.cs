using System.Data;

namespace Day14.Models
{
    public class Sand : IPlaceable
    {
        private readonly SandSpawn sandSpawn;

        public int Id { get; }
        
        public int CoordinatesRow { get; set; }

        public int CoordinatesColumn { get; set; }

        public bool IsStable { get; set; }

        public Sand(int id, SandSpawn sandSpawn)
        {
            this.sandSpawn = sandSpawn;
            IsStable = false;
            Id = id;
        }

        public void MoveTo(int coordinatesRow, int coordinatesColumn)
        {
            if (CoordinatesRow == Map.KillY)
            {
                sandSpawn.KillYReached();
            }
            
            CoordinatesRow = coordinatesRow;
            CoordinatesColumn = coordinatesColumn;
            
            while (Map.IsAreaBelowEmpty(CoordinatesRow, CoordinatesColumn))
            {
                FallDown();
            }
            
            if (Map.IsAreaBelowLeftEmpty(CoordinatesRow, CoordinatesColumn))
            {
                FallDownLeft();
                return;
            }
            
            if (Map.IsAreaBelowRightEmpty(CoordinatesRow, CoordinatesColumn))
            {
                FallDownRight();
                return;
            }

            IsStable = true;
            sandSpawn.SpawnedSandIsStable();
        }

        private void FallDown()
        {
            CoordinatesRow += 1;
        }
        
        private void FallDownLeft()
        {
            MoveTo(CoordinatesRow + 1, CoordinatesColumn - 1);
        }
        
        private void FallDownRight()
        {
            MoveTo(CoordinatesRow + 1, CoordinatesColumn + 1);
        }
    }
}