using System;
using System.Collections.Generic;
using System.Linq;

namespace Day16.Tools.Math
{
    public class GroupRange
    {
        private readonly IList<CustomRange> _intervals;

        public bool IsUnitary => _intervals.Count == 1;

        public GroupRange()
        {
            _intervals = new List<CustomRange>();
        }

        public void AddToGroup( CustomRange range )
        {
            _intervals.Add(range);
        }

        public bool IsIncluded( int number )
        {
            return _intervals.Any( x => x.Includes( number ) );
        }

        public int GetNotIncludedValue()
        {
            if ( IsUnitary )
            {
                throw new ApplicationException( "Group is unitary." );
            }

            return _intervals.First().Maximum + 1;
        }

        public void Reduce()
        {
            var orderedIntervals = _intervals.OrderBy( x => x.Minimum ).ToList();

            for ( int i = 0; i < orderedIntervals.Count - 1; i++ )
            {
                if ( orderedIntervals[i].Minimum == orderedIntervals[i + 1].Minimum )
                {
                    CustomRange newRange =
                        new CustomRange( orderedIntervals[i].Minimum, System.Math.Max( orderedIntervals[i].Maximum, orderedIntervals[i + 1].Maximum ) );
                    
                    _intervals.Remove( orderedIntervals[i] );
                    _intervals.Remove( orderedIntervals[i + 1] );
                    
                    _intervals.Add(newRange);
                    
                    orderedIntervals = _intervals.OrderBy( x => x.Minimum ).ToList();
                    i--;
                    continue;
                }
                
                if ( orderedIntervals[i].Maximum == orderedIntervals[i + 1].Maximum )
                {
                    CustomRange newRange =
                        new CustomRange( System.Math.Min( orderedIntervals[i].Minimum, orderedIntervals[i + 1].Minimum ),
                                        orderedIntervals[i].Maximum );
                    
                    _intervals.Remove( orderedIntervals[i] );
                    _intervals.Remove( orderedIntervals[i + 1] );
                    
                    _intervals.Add(newRange);
                    
                    orderedIntervals = _intervals.OrderBy( x => x.Minimum ).ToList();
                    i--;
                    continue;
                }

                if ( orderedIntervals[i].Includes(orderedIntervals[i + 1]) )
                {
                    _intervals.Remove( orderedIntervals[i + 1] );
                    
                    orderedIntervals = _intervals.OrderBy( x => x.Minimum ).ToList();
                    i--;
                    continue;
                }
                
                if ( orderedIntervals[i + 1].Includes(orderedIntervals[i].Maximum) )
                {
                    CustomRange newRange =
                        new CustomRange( orderedIntervals[i].Minimum, orderedIntervals[i + 1].Maximum );
                    
                    _intervals.Remove( orderedIntervals[i] );
                    _intervals.Remove( orderedIntervals[i + 1] );
                    
                    _intervals.Add(newRange);
                    
                    orderedIntervals = _intervals.OrderBy( x => x.Minimum ).ToList();
                    i--;
                    continue;
                }

                if ( orderedIntervals[i].Maximum + 1 == orderedIntervals[i+1].Minimum )
                {
                    CustomRange newRange =
                        new CustomRange( orderedIntervals[i].Minimum, orderedIntervals[i + 1].Maximum );
                    
                    _intervals.Remove( orderedIntervals[i] );
                    _intervals.Remove( orderedIntervals[i + 1] );
                    
                    _intervals.Add(newRange);
                    
                    orderedIntervals = _intervals.OrderBy( x => x.Minimum ).ToList();
                    i--;
                    continue;
                }
            }
        }
    }
}