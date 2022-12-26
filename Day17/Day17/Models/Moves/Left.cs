using Day17.Tools.Math;

namespace Day17.Models.Moves
{
    public class Left : IMove
    {
        private const int _modifierColumn = -1;
        private static readonly Right inverse = new Right();

        
        public void Perform( IPlaceable placeable )
        {
            placeable.CoordinateColumn += _modifierColumn;
        }

        public void Rollback(IPlaceable placeable)
        {
            inverse.Perform(placeable);
        }
    }
}