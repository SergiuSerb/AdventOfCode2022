namespace Day1.Models
{
    public class Backpack
    {
        private IList<Item> items;

        public Backpack()
        {
            items = new List<Item>();
        }

        public int TotalCalories => items.Sum( x => x.Calories );

        public void AddItem( Item itemToAdd )
        {
            items.Add(itemToAdd);
        }
    }
}