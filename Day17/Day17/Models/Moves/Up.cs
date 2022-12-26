using Day17.Tools.Math;

namespace Day17.Models.Moves
{
    public class Up : IMove
    {
        private const int _modifierRow = 1;
        private static readonly Down inverse = new Down();
        
        public void Perform( IPlaceable placeable )
        {
            placeable.CoordinateRow += _modifierRow;
        }

        public void Rollback(IPlaceable placeable)
        {
            inverse.Perform(placeable);
        }
    }
}