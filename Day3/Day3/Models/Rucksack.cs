namespace Day3.Models
{
    public class Rucksack
    {
        private Compartment FirstCompartment { get; }

        private Compartment SecondCompartment { get; }

        public Rucksack()
        {
            FirstCompartment = new Compartment();
            SecondCompartment = new Compartment();
        }

        public void AddToFirstCompartment(Item item)
        {
            FirstCompartment.AddItem( item );
        }

        public void AddToSecondCompartment(Item item)
        {
            SecondCompartment.AddItem( item );
        }

        public Item FindCommonItem()
        {
            return FirstCompartment.Items.First(x => SecondCompartment.Items.Any(y => y.Representation == x.Representation));
        }

        public IList<Item> GetCarriedItems()
        {
            IList<Item> carriedItems = new List<Item>();
            
            FirstCompartment.Items.ForEach(carriedItems.Add);
            SecondCompartment.Items.ForEach(carriedItems.Add);

            return carriedItems;
        }
    }
}