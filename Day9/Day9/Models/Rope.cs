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
            Tail.Print();
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
        
        public void Print()
        {
            int maxX = ropePoints.Max(x => x.CurrentPosition.X);
            int minX = ropePoints.Min(x => x.CurrentPosition.X);
            int maxY = ropePoints.Max(x => x.CurrentPosition.Y);
            int minY = ropePoints.Min(x => x.CurrentPosition.Y);

            int offsetX = 0;
            int offsetY = 0;

            if (minX < 0)
            {
                offsetX = Math.Abs(minX);
                maxX = maxX + offsetX;
            }
            
            if (minY < 0)
            {
                offsetY = Math.Abs(minY);
                maxY = maxY + offsetY;
            }

            string[,] representation = new string[15,15];

            for (int i = 0; i < representation.GetLength(0); i++)
            {
                for (int j = 0; j < representation.GetLength(1); j++)
                {
                    representation[i, j] = ".";
                }
            }

            int currentRopePointPosition = 0;
            foreach (RopePoint ropePoint in ropePoints)
            {
                representation[ropePoint.CurrentPosition.X + offsetX, ropePoint.CurrentPosition.Y + offsetY] = currentRopePointPosition.ToString();
                currentRopePointPosition++;
            }
            
            for (int i = 0; i < representation.GetLength(0); i++)
            {
                for (int j = 0; j < representation.GetLength(1); j++)
                {
                    Console.Write(representation[i, j]);
                }
                Console.WriteLine();
            }
            
            Console.WriteLine();
        }
    }
}