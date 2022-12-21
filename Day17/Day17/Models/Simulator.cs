using Day17.Models.Rocks;

namespace Day17.Models
{
    public class Simulator
    {
        public void Run()
        {
            for ( int rockIndex = 1; rockIndex <= 2022; rockIndex++ )
            {
                Rock rock = RockFactory.Create(rockIndex);
            }
        }
        
    }
}