using Day17.Tools.Math;

namespace Day17.Models.Moves
{
    public class Left : IMove
    {
        private const int _modifierColumn = -1;
        
        public void Perform( IPlaceable placeable )
        {
            placeable.CoordinateColumn += _modifierColumn;
        }
    }
}