namespace Day14.Models
{
    public class Sand : IPlaceable
    {
        public int Id { get; }
        
        public int CoordinatesRow { get; set; }

        public int CoordinatesColumn { get; set; }

        public Sand(int id)
        {
            Id = id;
        }

        public void MoveTo(int coordinatesRow, int coordinatesColumn)
        {
            CoordinatesRow = coordinatesRow;
            CoordinatesColumn = coordinatesColumn;
            
            NextMove nextMove = Map.GetNextMove( this );

            while ( nextMove.CanFall )
            {
                Fall( nextMove );
                nextMove = Map.GetNextMove( this );
            }
        }

        private void Fall( NextMove nextMove )
        {
            if ( nextMove.CanMoveDown  )
            {
                FallDown();
                return;
            }

            if ( nextMove.CanMoveDownLeft )
            {
                FallDownLeft();
                return;
            }

            if ( nextMove.CanMoveDownRight )
            {
                FallDownRight();
            }
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