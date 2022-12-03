namespace Day3.Models
{
    public class Compartment
    {
        private readonly List<Item> items;

        public Compartment()
        {
            items = new List<Item>();
        }

        public void AddItem(Item item)
        {
            items.Add(item);
        }

        public List<Item> GetContainingItems()
        {
            return items;
        }
        
    }
}