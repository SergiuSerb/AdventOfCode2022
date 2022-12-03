namespace Day3.Models
{
    public class Rucksack
    {
        private readonly Compartment firstCompartment;
        private readonly Compartment secondCompartment;

        public Rucksack()
        {
            firstCompartment = new Compartment();
            secondCompartment = new Compartment();
        }

        public void AddToFirstCompartment(Item item)
        {
            firstCompartment.AddItem( item );
        }

        public void AddToSecondCompartment(Item item)
        {
            secondCompartment.AddItem( item );
        }

        public Item FindCommonItem()
        {
            return firstCompartment.GetContainingItems()
                .First(x => secondCompartment.GetContainingItems().Any(y => y.Representation == x.Representation));
        }

        public IList<Item> GetCarriedItems()
        {
            IList<Item> carriedItems = new List<Item>();
            
            firstCompartment.GetContainingItems().ForEach(carriedItems.Add);
            secondCompartment.GetContainingItems().ForEach(carriedItems.Add);

            return carriedItems;
        }
    }
}