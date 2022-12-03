namespace Day3.Models
{
    public class Elf
    {
        public int Id { get; set; }
        
        public Rucksack Rucksack { get; set; }

        public Elf(int id)
        {
            Id = id;
            Rucksack = new Rucksack();
        }

        public void AddItemToRucksackFirstCompartment(Item item)
        {
            Rucksack.AddToFirstCompartment(item);
        }

        public void AddItemToRucksackSecondCompartment(Item item)
        {
            Rucksack.AddToSecondCompartment(item);
        }

        public Item GetMisplacedItem()
        {
            return Rucksack.FindCommonItem();
        }

        public IList<Item> GetCarriedItems()
        {
            return Rucksack.GetCarriedItems();
        }
    }
}