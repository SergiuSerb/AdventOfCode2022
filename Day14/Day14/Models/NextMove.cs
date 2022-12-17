namespace Day14.Models
{
    public class NextMove
    {
        public bool CanMoveDown { get; }

        public bool CanMoveDownRight { get; }

        public bool CanMoveDownLeft { get; }

        public bool HasReachedFloorY { get; }

        public bool CanFall => ( CanMoveDown || CanMoveDownRight || CanMoveDownLeft ) && !HasReachedFloorY;

        public NextMove( bool canMoveDown, bool canMoveDownRight, bool canMoveDownLeft, bool hasReachedFloorY )
        {
            CanMoveDown = canMoveDown;
            CanMoveDownRight = canMoveDownRight;
            CanMoveDownLeft = canMoveDownLeft;
            HasReachedFloorY = hasReachedFloorY;
        }
    }
}