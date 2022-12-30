using System.Collections.Generic;
using System.Linq;
using Day17.Models.Rocks;
using Day17.Tools.Math;

namespace Day17.Models
{
    public static class CollisionCalculator
    {
        private static readonly CustomRange wallRange = new CustomRange(1, 7);
        private static readonly CustomRange floorRange = new CustomRange(-1, -1000);
        
        public static bool DoesCollide( IPlaceable actor, List<RockComponent> mapChunk )
        {
            Rock actorAsRock = actor as Rock;

            BoundingBox rockBoundingBox = actorAsRock.GetBoundingBoxInWorldCoords();

            if ( !wallRange.Includes(rockBoundingBox.ColumnRange) || 
                floorRange.Includes(rockBoundingBox.RowRange))
            {
                return true;
            }

            IList<RockComponent> rockComponents = actorAsRock.GetRockComponentsInWorldCoordinates();
            
            foreach (RockComponent rockComponent in rockComponents)
            {
                if (mapChunk.Any(x => x.CoordinateRow == rockComponent.CoordinateRow && x.CoordinateColumn == rockComponent.CoordinateColumn))
                {
                    return true;
                }
            }

            return false;
        }
    }
}