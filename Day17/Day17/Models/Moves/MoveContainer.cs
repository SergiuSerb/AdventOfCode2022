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

        public int MoveCount => _moves.Count;

        public IMove GetNextMove()
        {
            _currentMoveIndex++;
            return _moves[_currentMoveIndex % _moves.Count];
        }

        public void Reset()
        {
            _currentMoveIndex = -1;
        }
    }
}