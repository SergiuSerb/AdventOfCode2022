using System.Collections.Generic;
using System.Linq;
using Day17.Models.Rocks;
using Day17.Tools.Math;

namespace Day17.Models
{
    public static class CollisionCalculator
    {
        public static bool DoesCollide( IPlaceable actor, List<IIdentifiablePlaceable> map )
        {
            IEnumerable<BoundingBox> idk = PerformSimpleCollisionCheck( actor, map );
            if ( idk.Any() )
            {
                return PerformComplexCollisionCheck( actor, idk );
            }

            return false;
        }

        private static bool PerformComplexCollisionCheck( IPlaceable actor, List<IIdentifiablePlaceable> map )
        {
            Rock rock = actor as Rock;
        }

        private static IEnumerable<BoundingBox> PerformSimpleCollisionCheck( IPlaceable actor, List<IIdentifiablePlaceable> placeables )
        {
            Rock rock = actor as Rock;

            BoundingBox actorBoundingBox = rock?.GetBoundingBoxInWorldCoords();

            return placeables.Select( x => (x as Rock).GetBoundingBoxInWorldCoords() )
                             .Where( x => x.Intersects( actorBoundingBox ) );
        }
    }
}