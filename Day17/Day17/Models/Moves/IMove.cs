using Day17.Tools.Math;

namespace Day17.Models.Moves
{
    public interface IMove
    {
        void Perform( IPlaceable placeable );
        
        void Rollback( IPlaceable placeable );
    }
}