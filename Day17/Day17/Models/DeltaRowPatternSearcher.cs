using System;
using System.Collections.Generic;
using System.Linq;

namespace Day17.Models
{
    public class DeltaRowPatternSearcher
    {
        private readonly List<Pattern> patterns = new List<Pattern>();
        
        public List<Pattern> Search(IList<LogEntry> logEntries)
        {
            string fullSequenceRepresentation = GetStringRepresentation(logEntries);
            
            for (int startingRockIndex = logEntries.Count - 1; startingRockIndex >= 0; startingRockIndex--)
            {
                for (int sequenceLength = 1; sequenceLength < Math.Sqrt(logEntries.Count - startingRockIndex); sequenceLength++)
                {
                    IEnumerable<LogEntry> sequence = logEntries.Skip(startingRockIndex).Take(sequenceLength);

                    string sequenceRepresentation = GetStringRepresentation(sequence);
                    
                    string temporaryFullSequenceRepresentation = fullSequenceRepresentation.Replace(sequenceRepresentation, "X");

                    int occurenceCount = temporaryFullSequenceRepresentation.Count(x => x == 'X');

                    int occurrencesFromEnd = temporaryFullSequenceRepresentation.Reverse().TakeWhile(x => x is 'X').Count();
                    
                
                    if (occurenceCount > 1)
                    {
                        patterns.Add(new Pattern(sequenceRepresentation, occurenceCount, occurrencesFromEnd, startingRockIndex, sequenceLength));
                    }
                }
            }

            return patterns;
        }

        private static string GetStringRepresentation(IEnumerable<LogEntry> logEntries)
        {
            string representation = logEntries.Aggregate(string.Empty, (current, entry) => $"{current}{entry}");

            return representation;
        }
    }
}