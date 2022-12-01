namespace Day1.Models
{
    public class Elf
    {
        public int Id { get; set; }

        public Backpack Backpack { get; set; }

        public int CurrentlyCarriedCalories => Backpack.TotalCalories;

        public Elf( int id )
        {
            Id = id;
            Backpack = new Backpack();
        }

        public void PickItem( Item item )
        {
            Backpack.AddItem(item);
        }
        
    }
}