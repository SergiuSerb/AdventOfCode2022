using System.Collections.Generic;

namespace Day17.Models.Moves
{
    public class MoveContainer
    {
        private readonly List<IMove> _moves;

        private int _currentMoveIndex;

        public MoveContainer( List<IMove> moves )
        {
            _moves = moves;
            _currentMoveIndex = -1;
        }

        public IMove GetNextMove()
        {
            _currentMoveIndex++;
            return _moves[_currentMoveIndex % _moves.Count];
        }
    }
}