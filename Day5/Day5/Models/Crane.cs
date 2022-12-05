using System.Collections.Generic;

namespace Day5.Models
{
    public abstract class Crane
    {
        protected IList<CrateStack> Stacks { get; }

        protected Crane(IList<CrateStack> stacks)
        {
            Stacks = stacks;
        }

        public abstract void Move(Move move);
    }
}