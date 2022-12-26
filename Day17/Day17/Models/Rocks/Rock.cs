using System.Collections.Generic;
using System.Linq;
using Day17.Tools.Math;

namespace Day17.Models.Rocks
{
    public class Rock : IIdentifiablePlaceable
    {
        private IList<RockComponent> _components;

        public long CoordinateRow { get; set; }

        public long CoordinateColumn { get; set; }
        
        public long RowSpan { get; set; }

        protected IList<RockComponent> Components
        {
            get => _components;
            set
            {
                _components = value;
                

                RowSpan = _components.Max(x => x.CoordinateRow) - _components.Min(x => x.CoordinateRow);
            }
        }

        public ulong Id { get; set; }
        
        public IList<RockComponent> GetRockComponentsInWorldCoordinates()
        {
            return Components
                   .Select( x => new RockComponent( x.CoordinateRow + CoordinateRow,
                                                   x.CoordinateColumn + CoordinateColumn ) ).ToList();
        }

        public BoundingBox GetBoundingBoxInWorldCoords()
        {
            long boundingBoxRowMax = Components.Max( x => x.CoordinateRow + CoordinateRow );
            long boundingBoxRowMin = Components.Min( x => x.CoordinateRow + CoordinateRow );
            long boundingBoxColumnMax = Components.Max( x => x.CoordinateColumn + CoordinateColumn );
            long boundingBoxColumnMin = Components.Min( x => x.CoordinateColumn + CoordinateColumn );

            return new BoundingBox( new CustomRange( boundingBoxRowMin, boundingBoxRowMax ),
                                   new CustomRange( boundingBoxColumnMin, boundingBoxColumnMax ) );
        }


    }
}