namespace Day13.Models
{
    internal class SequenceGroup
    {
        public int Id { get; }
        
        public Sequence Left { get; }

        public Sequence Right { get; }

        public bool IsCorrect => GetIsCorrectOrder();

        private bool GetIsCorrectOrder()
        {
            int result = Sequence.Compare(Left, Right);

            return result == -1;
        }

        public SequenceGroup(int id, Sequence left, Sequence right)
        {
            Id = id;
            Left = left;
            Right = right;
        }
    }
}