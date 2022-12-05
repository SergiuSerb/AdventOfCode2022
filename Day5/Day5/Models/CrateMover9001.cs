using System.Collections.Generic;
using System.Linq;

namespace Day5.Models
{
    public class CrateMover9001 : Crane
    {
        public CrateMover9001(IList<CrateStack> stacks) : base(stacks)
        {
        }

        protected override void Move(int sourceStackId, int destinationStackId, int numberOfCratesToMove)
        {
            CrateStack source = Stacks.First(x => x.Id == sourceStackId);
            CrateStack destination  = Stacks.First(x => x.Id == destinationStackId);

            Stack<Crate> stackToMove = new Stack<Crate>();

            for (int crateIndex = 1; crateIndex <= numberOfCratesToMove; crateIndex++)
            {
                Crate removedCrate = source.Remove();
                stackToMove.Push(removedCrate);
            }

            foreach (Crate crate in stackToMove)
            {
                destination.Add(crate);
            }
        }

        public override void Move(Move move)
        {
            Move(move.SourceStackId, move.DestinationStackId, move.NumberOfCratesToMove);
        }
    }
}