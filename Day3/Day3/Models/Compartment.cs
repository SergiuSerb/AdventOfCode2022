namespace Day3.Models
{
    public class Compartment
    {
        public List<Item> Items { get; }

        public Compartment()
        {
            Items = new List<Item>();
        }

        public void AddItem(Item item)
        {
            Items.Add(item);
        }
        
    }
}