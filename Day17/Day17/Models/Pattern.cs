namespace Day17.Models
{
    public class Pattern
    {
        public readonly string Representation;
        public readonly int OccurenceCount;
        public readonly int OccurrenceFromEnd;
        public readonly int StartingIndex;
        public int SequenceLength;

        public Pattern(string representation, int occurenceCount, int occurrenceCountFromEnd, int startingIndex, int sequenceLength)
        {
            SequenceLength = sequenceLength;
            Representation = representation;
            OccurenceCount = occurenceCount;
            OccurrenceFromEnd = occurrenceCountFromEnd;
            StartingIndex = startingIndex;
        }
    }
}