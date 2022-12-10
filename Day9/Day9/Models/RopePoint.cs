using System;
using System.Collections.Generic;
using System.Linq;

namespace Day9.Models
{
    public class RopePoint
    {
        public int Id { get; }
        
        public Point CurrentPosition { get; private set; }

        private IList<PositionHistoryEntry> PositionHistory { get; }

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
            PositionHistory.Add(new PositionHistoryEntry(moveIndex, CurrentPosition));
            CurrentPosition = point;

            NextRopePoint?.RecalculatePosition(moveIndex);
        }

        private void RecalculatePosition(int moveIndex)
        {
            if ( ShouldMove())
            {
                if (CurrentPosition.X > PreviousRopePoint.CurrentPosition.X && CurrentPosition.Y > PreviousRopePoint.CurrentPosition.Y)
                {
                    MoveDownLeft(moveIndex);
                    return;
                }                  
                
                if (CurrentPosition.X == PreviousRopePoint.CurrentPosition.X && CurrentPosition.Y > PreviousRopePoint.CurrentPosition.Y)
                {
                    MoveLeft(moveIndex);
                    return;
                }                  
                
                if (CurrentPosition.X < PreviousRopePoint.CurrentPosition.X && CurrentPosition.Y > PreviousRopePoint.CurrentPosition.Y)
                {
                    MoveUpLeft(moveIndex);
                    return;
                }                  
                
                if (CurrentPosition.X < PreviousRopePoint.CurrentPosition.X && CurrentPosition.Y == PreviousRopePoint.CurrentPosition.Y)
                {
                    MoveUp(moveIndex);
                    return;
                }                  
                
                if (CurrentPosition.X < PreviousRopePoint.CurrentPosition.X && CurrentPosition.Y < PreviousRopePoint.CurrentPosition.Y)
                {
                    MoveUpRight(moveIndex);
                    return;
                }                  
                
                if (CurrentPosition.X == PreviousRopePoint.CurrentPosition.X && CurrentPosition.Y < PreviousRopePoint.CurrentPosition.Y)
                {
                    MoveRight(moveIndex);
                    return;
                }                  
                
                if (CurrentPosition.X > PreviousRopePoint.CurrentPosition.X && CurrentPosition.Y < PreviousRopePoint.CurrentPosition.Y)
                {
                    MoveDownRight(moveIndex);
                    return;
                }                  
                
                if (CurrentPosition.X > PreviousRopePoint.CurrentPosition.X && CurrentPosition.Y == PreviousRopePoint.CurrentPosition.Y)
                {
                    MoveDown(moveIndex);
                    return;
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

        public IEnumerable<Point> GetUniquePositions()
        {
            List<PositionHistoryEntry> positionsToSearch = new List<PositionHistoryEntry>(PositionHistory)
            {
                new PositionHistoryEntry(0, CurrentPosition)
            };

            return positionsToSearch.GroupBy(x => x.Position.Id).Select(x => x.First().Position);
        }
    }
}