using System;
using System.Data;

namespace Day14.Models
{
    public class Sand : IPlaceable
    {
        private readonly SandSpawn _sandSpawn;

        public int Id { get; }
        
        public int CoordinatesRow { get; set; }

        public int CoordinatesColumn { get; set; }

        public bool IsStable { get; set; }

        public Sand(int id, SandSpawn sandSpawn)
        {
            _sandSpawn = sandSpawn;
            IsStable = false;
            Id = id;
        }

        public void MoveTo(int coordinatesRow, int coordinatesColumn)
        {
            CoordinatesRow = coordinatesRow;
            CoordinatesColumn = coordinatesColumn;

            while ( Map.CanMoveDown(this) )
            {
                if ( HasSandReachedSandSpawn() )
                {
                    break;
                }

                Fall();
            }
            
            IsStable = true;
            _sandSpawn.SpawnedSandIsStable();
        }

        private void Fall()
        {
            while ( Map.IsAreaBelowEmpty( CoordinatesRow, CoordinatesColumn ) )
            {
                if ( HasSandReachedSandSpawn() )
                {
                    return;
                }
                
                FallDown();
            }

            if ( Map.IsAreaBelowLeftEmpty( CoordinatesRow, CoordinatesColumn ) )
            {
                FallDownLeft();
                return;
            }

            if ( Map.IsAreaBelowRightEmpty( CoordinatesRow, CoordinatesColumn ) )
            {
                FallDownRight();
            }
        }
        
        private bool HasSandReachedSandSpawn()
        {
            if ( Map.HasSandReachedSandSpawn(this) && IsStable)
            {
                _sandSpawn.SandSpawnReached();
                return true;
            }

            return false;
        }

        private void FallDown()
        {
            CoordinatesRow += 1;
        }
        
        private void FallDownLeft()
        {
            CoordinatesRow += 1;
            CoordinatesColumn -= 1;
        }
        
        private void FallDownRight()
        {
            CoordinatesRow += 1;
            CoordinatesColumn += 1;
        }
    }
}