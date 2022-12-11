using System.Numerics;

namespace Day11.Models
{
    public class Item
    {
        public int Id { get; }

        public ulong Importance { get; set; }

        public Item( int id, ulong importance )
        {
            Id = id;
            Importance = importance;
        }
    }
}