using System;
using System.Collections.Generic;
using System.Linq;

namespace Day17.Models
{
    public class DeltaRowPatternSearcherV2
    {
        private readonly List<Pattern> patterns = new List<Pattern>();
        
        public List<Pattern> Search(IList<LogEntry> logEntries)
        {
            string fullSequenceRepresentation = GetStringRepresentation(logEntries);

            for (int startingRockIndex = logEntries.Count - 1; startingRockIndex >= (logEntries.Count / 10) * 9; startingRockIndex--)
            {
                int sequenceLength = logEntries.Count - startingRockIndex;
                
                if (sequenceLength % 1000 == 0)
                {
                    Console.WriteLine($"{sequenceLength}/{logEntries.Count}");
                }
                
                IEnumerable<LogEntry> sequence = logEntries.Skip(startingRockIndex).Take(sequenceLength);

                string sequenceRepresentation = GetStringRepresentation(sequence);
                    
                string temporaryFullSequenceRepresentation = fullSequenceRepresentation.Replace(sequenceRepresentation, "X");

                int occurrencesFromEnd = temporaryFullSequenceRepresentation.Reverse().TakeWhile(x => x is 'X').Count();

                if (occurrencesFromEnd > 1)
                {
                    int occurenceCount = temporaryFullSequenceRepresentation.Count(x => x == 'X');
                        
                    Pattern pattern = new Pattern(sequenceRepresentation, occurenceCount, occurrencesFromEnd, startingRockIndex, sequenceLength);
                    patterns.Add(pattern);
                }
            }

            return patterns;
        }

        public List<Pattern> GetALlPatterns()
        {
            return patterns;
        }

        private static string GetStringRepresentation(IEnumerable<LogEntry> logEntries)
        {
            string representation = logEntries.Aggregate(string.Empty, (current, entry) => $"{current}{entry}");

            return representation;
        }
    }
}