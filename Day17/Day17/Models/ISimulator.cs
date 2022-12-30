using Day17.Models.Moves;

namespace Day17.Models
{
    public interface ISimulator
    {
        ulong Run(MoveContainer moveContainer, ulong rocksToSpawn);
    }
}