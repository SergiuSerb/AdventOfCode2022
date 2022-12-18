using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Day15.Tools.Math;

namespace Day15.Models
{
    public class Map
    {
        private readonly IList<IPlaceable> _items;

        public Map()
        {
            _items = new List<IPlaceable>();
        }

        public void AddItem( IPlaceable itemToAdd )
        {
            _items.Add(itemToAdd);
        }

        public int FindScannedTilesAtLine( int lineIndex )
        {
            IList<Sensor> involvedSensors = _items.OfType<Sensor>()
                                                  .Where( x => x.CoordinateRow + x.DistanceToBeacon >= lineIndex &&
                                                               x.CoordinateRow - x.DistanceToBeacon <= lineIndex )
                                                  .ToList();

            int minColumnCoord = involvedSensors.Min( x => x.CoordinateColumn - x.DistanceToBeacon );
            int maxColumnCoord = involvedSensors.Max( x => x.CoordinateColumn + x.DistanceToBeacon );
            
            Console.WriteLine($"Checking line {lineIndex} starting column {minColumnCoord} through {maxColumnCoord}.");

            int checkedTilesCount = 0;
            CheckTile checkTile = new CheckTile(0, 0 );
            for ( int columnCoord = minColumnCoord; columnCoord <= maxColumnCoord; columnCoord++ )
            {
                checkTile.MoveTo(lineIndex, columnCoord );
                
                foreach ( Sensor involvedSensor in involvedSensors )
                {
                    if ( MathHelper.ManhattanDistance(involvedSensor.ClosestBeacon, checkTile) == 0)
                    {
                        break;
                    }
                    
                    if ( MathHelper.ManhattanDistance(checkTile, involvedSensor) <= involvedSensor.DistanceToBeacon )
                    {
                        checkedTilesCount++;
                        break;
                    }
                }
            }
            
            return checkedTilesCount;
        }
        
        public BigInteger FindUnscannedTilesInBoundingBox( int minRowColumn, int maxRowColumn )
        {
            IList<Sensor> involvedSensors = _items.OfType<Sensor>().ToList();

            for ( int rowIndex = minRowColumn; rowIndex <= maxRowColumn; rowIndex++ )
            {
                if ( rowIndex % 1000000 == 0 )
                {
                    Console.WriteLine($"Scanning row {rowIndex}.");
                }
                
                GroupRange group = new GroupRange();
                foreach ( Sensor involvedSensor in involvedSensors )
                {
                    if ( Math.Abs(involvedSensor.CoordinateRow - rowIndex) > involvedSensor.DistanceToBeacon  )
                    {
                        continue;
                    }

                    int offset = involvedSensor.DistanceToBeacon - Math.Abs( involvedSensor.CoordinateRow - rowIndex );
                    CustomRange range = new CustomRange( involvedSensor.CoordinateColumn - offset,
                                                        involvedSensor.CoordinateColumn + offset );
                    
                    group.AddToGroup(range);
                }
                
                group.Reduce();

                if ( !group.IsUnitary )
                {
                    Console.WriteLine($"Non-unitary at {rowIndex}, {group.GetNotIncludedValue()}.");
                    return GetTuningFrequencyOfTile( new CheckTile( rowIndex, group.GetNotIncludedValue() ) );
                }
            }

            return 0;
        }

        private BigInteger GetTuningFrequencyOfTile( CheckTile checkTile )
        {
            BigInteger coordinateColumn = new BigInteger( checkTile.CoordinateColumn );
            BigInteger coordinateRow = new BigInteger( checkTile.CoordinateRow );
            BigInteger multiplier = new BigInteger( 4000000 );
            
            return BigInteger.Add( BigInteger.Multiply(coordinateColumn, multiplier), coordinateRow);
        }
    }
}