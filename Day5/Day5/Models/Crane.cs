using System.Collections.Generic;
using System.Linq;

namespace Day5.Models
{
    public abstract class Crane
    {
        public IList<CrateStack> Stacks { get; }

        public Crane(IList<CrateStack> stacks)
        {
            Stacks = stacks;
        }

        protected abstract void Move(int sourceStackId, int destinationStackId, int numberOfCratesToMove);

        public abstract void Move(Move move);
    }
}