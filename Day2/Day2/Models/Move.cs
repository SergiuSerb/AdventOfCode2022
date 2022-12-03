namespace Day2.Models
{
    public abstract class Move
    {
        protected char Id { get; }

        protected string Name { get; }

        protected Move(char id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}