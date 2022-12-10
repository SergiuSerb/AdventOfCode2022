using System;
using System.Collections.Generic;
using System.Linq;

namespace Day9.Models
{
    public class Rope
    {
        private List<RopePoint> ropePoints;

        public RopePoint Head { get; private set; }

        public RopePoint Tail { get; private set; }
        
        public int RopePointsCount { get; }

        public Rope(int ropePointsCount)
        {
            RopePointsCount = ropePointsCount;
            BuildRope(ropePointsCount);
        }
        
        private void BuildRope(int ropePointsCount)
        {
            Point startingPoint = new Point(0, 0);

            ropePoints = new List<RopePoint>()
            {
                new RopePoint(startingPoint, 0)
            };
            
            for (int i = 0; i < ropePointsCount - 1; i++)
            {
                RopePoint newRopePoint = new RopePoint(startingPoint, i + 1)
                {
                    PreviousRopePoint = ropePoints[i]
                };
                
                ropePoints[i].NextRopePoint = newRopePoint;
                ropePoints.Add(newRopePoint);
            }

            Head = ropePoints.First();
            Tail = ropePoints.Last();
        }

        public IList<Point> GetUniqueTailPositions()
        {
            return Tail.GetUniquePositions().ToList();
        }

        public void MoveUp(int moveIndex)
        {
            Head.MoveUp(moveIndex);
        }        
        
        public void MoveDown(int moveIndex)
        {
            Head.MoveDown(moveIndex);
        }        
        
        public void MoveLeft(int moveIndex)
        {
            Head.MoveLeft(moveIndex);
        }        
        
        public void MoveRight(int moveIndex)
        {
            Head.MoveRight(moveIndex);
        }
    }
}