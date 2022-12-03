namespace Day3.Models
{
    public class Elf
    {
        private int id;
        private readonly Rucksack rucksack;

        public Elf(int id)
        {
            this.id = id;
            rucksack = new Rucksack();
        }

        public void AddItemToRucksackFirstCompartment(Item item)
        {
            rucksack.AddToFirstCompartment(item);
        }

        public void AddItemToRucksackSecondCompartment(Item item)
        {
            rucksack.AddToSecondCompartment(item);
        }

        public Item GetMisplacedItem()
        {
            return rucksack.FindCommonItem();
        }

        public IList<Item> GetCarriedItems()
        {
            return rucksack.GetCarriedItems();
        }
    }
}