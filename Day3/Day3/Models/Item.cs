namespace Day3.Models
{
    public class Item
    {
        private int Id { get; }

        public char Representation { get; }

        public int Priority => GetPriority();

        public Item(int id, char representation)
        {
            Id = id;
            Representation = representation;
        }

        private int GetPriority()
        {
            if (char.IsLower(Representation))
            {
                return Representation - 'a' + 1;
            }

            return Representation - 'A' + 27;
        }
    }
}