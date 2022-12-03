namespace Day3.Models
{
    public class ElfGroup
    {
        private const int Capacity = 3;

        public int Id { get; }

        public IList<Elf> Elves { get; }

        public ElfGroup(int id)
        {
            Id = id;
            Elves = new List<Elf>();
        }

        public void AddToGroup(Elf elf)
        {
            Elves.Add(elf);
        }

        public Item GetBadgeItem()
        {
            Elf firstElf = Elves[0];

            foreach (Item carriedItem in firstElf.GetCarriedItems())
            {
                bool doesSecondElfHaveItem = Elves[1].GetCarriedItems().Any(x => x.Representation == carriedItem.Representation);
                bool doesThirdElfHaveItem = Elves[2].GetCarriedItems().Any(x => x.Representation == carriedItem.Representation);

                if (doesSecondElfHaveItem && doesThirdElfHaveItem)
                {
                    return carriedItem;
                }
                
            }

            return null;
        }

        public bool IsFull()
        {
            return Elves.Count >= Capacity;
        }

    }
}