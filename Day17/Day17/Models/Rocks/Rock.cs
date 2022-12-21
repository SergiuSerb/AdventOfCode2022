using System.Collections.Generic;
using System.Linq;
using Day17.Tools.Math;

namespace Day17.Models.Rocks
{
    public class Rock : IIdentifiablePlaceable
    {
        public int CoordinateRow { get; set; }

        public int CoordinateColumn { get; set; }

        protected IList<RockComponent> components;

        public int Id { get; set; }
        
        public IList<RockComponent> GetRockComponentsInWorldCoordinates()
        {
            return components
                   .Select( x => new RockComponent( x.CoordinateRow + CoordinateRow,
                                                   x.CoordinateColumn + CoordinateColumn ) ).ToList();
        }

        public BoundingBox GetBoundingBoxInWorldCoords()
        {
            int boundingBoxRowMax = components.Max( x => x.CoordinateRow + CoordinateRow );
            int boundingBoxRowMin = components.Min( x => x.CoordinateRow + CoordinateRow );
            int boundingBoxColumnMax = components.Max( x => x.CoordinateColumn + CoordinateColumn );
            int boundingBoxColumnMin = components.Min( x => x.CoordinateColumn + CoordinateColumn );

            return new BoundingBox( new CustomRange( boundingBoxRowMin, boundingBoxRowMax ),
                                   new CustomRange( boundingBoxColumnMin, boundingBoxColumnMax ) );
        }


    }
}