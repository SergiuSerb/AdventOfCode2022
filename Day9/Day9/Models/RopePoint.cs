using System;
using System.Collections.Generic;
using System.Linq;

namespace Day9.Models
{
    public class RopePoint
    {
        public int Id { get; set; }
        
        public Point CurrentPosition { get; private set; }
        
        public IList<PositionHistoryEntry> PositionHistory { get; }

        public RopePoint PreviousRopePoint { get; set; }
        
        public RopePoint NextRopePoint { get; set; }

        public RopePoint(Point currentPosition, int id)
        {
            Id = id;
            CurrentPosition = currentPosition;
            PositionHistory = new List<PositionHistoryEntry>();
        }

        private void MoveTo(int moveIndex, Point point)
        {
            Console.WriteLine($"Start performing move {moveIndex} by {Id}.");
            PositionHistory.Add(new PositionHistoryEntry(moveIndex, CurrentPosition));
            CurrentPosition = point;
            
            Console.WriteLine($"Requesting refresh rope point id {Id} for {NextRopePoint.Id} for moveIndex {moveIndex}.");
            NextRopePoint.RefreshPosition(moveIndex);
            Console.WriteLine($"Stop performing move {moveIndex} by {Id}.");
        }

        private void RefreshPosition(int moveIndex)
        {
            if ( ShouldMove())
            {
                if (CurrentPosition.X > PreviousRopePoint.CurrentPosition.X && CurrentPosition.Y > PreviousRopePoint.CurrentPosition.Y)
                {
                    MoveDownLeft(moveIndex);
                }                  
                
                if (CurrentPosition.X == PreviousRopePoint.CurrentPosition.X && CurrentPosition.Y > PreviousRopePoint.CurrentPosition.Y)
                {
                    MoveLeft(moveIndex);
                }                  
                
                if (CurrentPosition.X < PreviousRopePoint.CurrentPosition.X && CurrentPosition.Y > PreviousRopePoint.CurrentPosition.Y)
                {
                    MoveUpLeft(moveIndex);
                }                  
                
                if (CurrentPosition.X < PreviousRopePoint.CurrentPosition.X && CurrentPosition.Y == PreviousRopePoint.CurrentPosition.Y)
                {
                    MoveUp(moveIndex);
                }                  
                
                if (CurrentPosition.X < PreviousRopePoint.CurrentPosition.X && CurrentPosition.Y < PreviousRopePoint.CurrentPosition.Y)
                {
                    MoveUpRight(moveIndex);
                }                  
                
                if (CurrentPosition.X == PreviousRopePoint.CurrentPosition.X && CurrentPosition.Y < PreviousRopePoint.CurrentPosition.Y)
                {
                    MoveRight(moveIndex);
                }                  
                
                if (CurrentPosition.X > PreviousRopePoint.CurrentPosition.X && CurrentPosition.Y < PreviousRopePoint.CurrentPosition.Y)
                {
                    MoveDownRight(moveIndex);
                }                  
                
                if (CurrentPosition.X > PreviousRopePoint.CurrentPosition.X && CurrentPosition.Y == PreviousRopePoint.CurrentPosition.Y)
                {
                    MoveDown(moveIndex);
                }
            }
        }

        private bool ShouldMove()
        {
            int offset = 1000;

            return Math.Max(Math.Abs((PreviousRopePoint.CurrentPosition.X + offset) - (CurrentPosition.X + offset)),
                Math.Abs((PreviousRopePoint.CurrentPosition.Y + offset) - (CurrentPosition.Y + offset))) > 1;
        }

        public void MoveUp(int moveIndex)
        {
            Point newPoint = new Point(CurrentPosition.X + 1, CurrentPosition.Y);
            MoveTo(moveIndex, newPoint);
        }

        private void MoveUpRight(int moveIndex)
        {
            Point newPoint = new Point(CurrentPosition.X + 1, CurrentPosition.Y + 1);
            MoveTo(moveIndex, newPoint);
        }   
        
        private void MoveUpLeft(int moveIndex)
        {
            Point newPoint = new Point(CurrentPosition.X + 1, CurrentPosition.Y - 1);
            MoveTo(moveIndex, newPoint);
        }  
        
        public void MoveDown(int moveIndex)
        {
            Point newPoint = new Point(CurrentPosition.X - 1, CurrentPosition.Y);
            MoveTo(moveIndex, newPoint);
        }  
        
        private void MoveDownRight(int moveIndex)
        {
            Point newPoint = new Point(CurrentPosition.X - 1, CurrentPosition.Y + 1);
            MoveTo(moveIndex, newPoint);
        }
        
        private void MoveDownLeft(int moveIndex)
        {
            Point newPoint = new Point(CurrentPosition.X - 1, CurrentPosition.Y - 1);
            MoveTo(moveIndex, newPoint);
        }
        
        public void MoveLeft(int moveIndex)
        {
            Point newPoint = new Point(CurrentPosition.X, CurrentPosition.Y - 1);
            MoveTo(moveIndex, newPoint);
        }        
        
        public void MoveRight(int moveIndex)
        {
            Point newPoint = new Point(CurrentPosition.X, CurrentPosition.Y + 1);
            MoveTo(moveIndex, newPoint);
        }

        private int DistanceToNextRopePoint()
        {
            var distanceX = CurrentPosition.X > NextRopePoint.CurrentPosition.X
                ? CurrentPosition.X - NextRopePoint.CurrentPosition.X
                : NextRopePoint.CurrentPosition.X - CurrentPosition.X;
            
            var distanceY = CurrentPosition.Y > NextRopePoint.CurrentPosition.Y
                ? CurrentPosition.Y - NextRopePoint.CurrentPosition.Y
                : NextRopePoint.CurrentPosition.Y - CurrentPosition.Y;
            
            //Console.WriteLine($"({distanceX},{distanceY})");

            return Math.Max(distanceX, distanceY);
        }

        public IEnumerable<Point> GetUniquePositions()
        {
            List<PositionHistoryEntry> positionsToSearch = new List<PositionHistoryEntry>(PositionHistory)
            {
                new PositionHistoryEntry(0, CurrentPosition)
            };

            return positionsToSearch.GroupBy(x => x.Position.Id).Select(x => x.First().Position);
        }

        public void Print()
        {
            List<PositionHistoryEntry> positionsToPrint = new List<PositionHistoryEntry>(PositionHistory)
            {
                new PositionHistoryEntry(0, CurrentPosition)
            };

            int maxX = positionsToPrint.Max(x => x.Position.X);
            int minX = positionsToPrint.Min(x => x.Position.X);
            int maxY = positionsToPrint.Max(x => x.Position.Y);
            int minY = positionsToPrint.Min(x => x.Position.Y);

            int offsetX = 0;
            int offsetY = 0;

            if (minX < 0 && maxX > 0)
            {
                offsetX = Math.Abs(minX);
                maxX = maxX + offsetX;
            }
            
            if (minY < 0 && maxY > 0)
            {
                offsetY = Math.Abs(minY);
                maxY = maxY + offsetY;
            }

            char[,] representation = new char[maxX + 1, maxY + 1];

            for (int i = 0; i < representation.GetLength(0); i++)
            {
                for (int j = 0; j < representation.GetLength(1); j++)
                {
                    representation[i, j] = '.';
                }
            }

            foreach (PositionHistoryEntry positionHistoryEntry in positionsToPrint)
            {
                representation[positionHistoryEntry.Position.X + offsetX, positionHistoryEntry.Position.Y + offsetY] = '#';
            }
            
            for (int i = 0; i < representation.GetLength(0); i++)
            {
                for (int j = 0; j < representation.GetLength(1); j++)
                {
                    Console.Write(representation[i, j]);
                }
                Console.WriteLine();
            }
        }
        
    }
}