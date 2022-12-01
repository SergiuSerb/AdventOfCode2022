namespace Day1.Models
{
    public class Elf
    {
        public int Id { get; }

        private readonly Backpack _backpack;

        public int CurrentlyCarriedCalories => _backpack.TotalCalories;

        public Elf( int id )
        {
            Id = id;
            _backpack = new Backpack();
        }

        public void PickItem( Item item )
        {
            _backpack.AddItem(item);
        }
        
    }
}