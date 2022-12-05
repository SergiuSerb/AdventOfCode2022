using System.Collections.Generic;

namespace Day5.Models
{
    public class CrateStack
    {
        public int Id { get; }

        public Stack<Crate> Crates { get; }

        public CrateStack(int id)
        {
            Id = id;
            Crates = new Stack<Crate>();
        }

        public void Add(Crate crate)
        {
            Crates.Push(crate);
        }

        public Crate Remove()
        {
            return Crates.Pop();
        }

        public Crate GetTopCrate()
        {
            return Crates.Peek();
        }
    }
}