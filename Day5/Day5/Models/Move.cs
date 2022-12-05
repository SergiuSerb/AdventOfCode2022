namespace Day5.Models
{
    public class Move
    {
        public int Id { get; }

        public int SourceStackId { get; }

        public int DestinationStackId { get; }

        public int NumberOfCratesToMove { get; }

        public Move(int id, int sourceStackId, int destinationStackId, int numberOfCratesToMove)
        {
            Id = id;
            SourceStackId = sourceStackId;
            DestinationStackId = destinationStackId;
            NumberOfCratesToMove = numberOfCratesToMove;
        }
    }
}