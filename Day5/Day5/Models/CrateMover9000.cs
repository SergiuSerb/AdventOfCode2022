using System.Collections.Generic;
using System.Linq;

namespace Day5.Models
{
    public class CrateMover9000 : Crane
    {
        public CrateMover9000(IList<CrateStack> stacks) : base(stacks)
        {
        }

        protected override void Move(int sourceStackId, int destinationStackId, int numberOfCratesToMove)
        {
            CrateStack source = Stacks.First(x => x.Id == sourceStackId);
            CrateStack destination  = Stacks.First(x => x.Id == destinationStackId);

            for (int crateIndex = 1; crateIndex <= numberOfCratesToMove; crateIndex++)
            {
                Crate removedCrate = source.Remove();
                destination.Add(removedCrate);
            }
        }

        public override void Move(Move move)
        {
            Move(move.SourceStackId, move.DestinationStackId, move.NumberOfCratesToMove);
        }
    }
}