namespace Day2.Models
{
    public abstract class Move
    {
        protected char Id { get; }

        protected string Name { get; }

        public int Score { get; }

        protected Move(char id, string name, int score)
        {
            Id = id;
            Name = name;
            Score = score;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}