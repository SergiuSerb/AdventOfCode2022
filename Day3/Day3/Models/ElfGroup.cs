namespace Day3.Models
{
    public class ElfGroup
    {
        private const int Capacity = 3;
        private int id;
        private readonly IList<Elf> elves;

        public ElfGroup(int id)
        {
            this.id = id;
            elves = new List<Elf>();
        }

        public void AddToGroup(Elf elf)
        {
            elves.Add(elf);
        }

        public Item GetBadgeItem()
        {
            Elf firstElf = elves[0];

            foreach (Item carriedItem in firstElf.GetCarriedItems())
            {
                bool doesSecondElfHaveItem = elves[1].GetCarriedItems().Any(x => x.Representation == carriedItem.Representation);
                bool doesThirdElfHaveItem = elves[2].GetCarriedItems().Any(x => x.Representation == carriedItem.Representation);

                if (doesSecondElfHaveItem && doesThirdElfHaveItem)
                {
                    return carriedItem;
                }
                
            }

            throw new ApplicationException("No badge item could be found in group.");
        }

        public bool IsFull()
        {
            return elves.Count >= Capacity;
        }

    }
}