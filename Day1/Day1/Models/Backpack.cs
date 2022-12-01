namespace Day1.Models
{
    public class Backpack
    {
        private readonly IList<Item> _items;

        public Backpack()
        {
            _items = new List<Item>();
        }

        public int TotalCalories => _items.Sum( x => x.Calories );

        public void AddItem( Item itemToAdd )
        {
            _items.Add(itemToAdd);
        }
    }
}