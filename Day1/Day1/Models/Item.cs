namespace Day1.Models
{
    public class Item
    {
        public int Id { get; }
        public int Calories { get; }

        public Item( int id, int calories )
        {
            Id = id;
            Calories = calories;
        }
    }
}