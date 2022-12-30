using System;
using System.Collections.Generic;
using System.Linq;

namespace Day17.Models
{
    public class StringBasedPatternSearcher
    {
        private List<Pattern> patterns = new List<Pattern>();
        
        public void Search(Dictionary<int, List<RockComponent>> getLastRun)
        {
            Dictionary<int, string> stringRepresentation = new Dictionary<int, string>();
            
            foreach (KeyValuePair<int,List<RockComponent>> pair in getLastRun)
            {
                stringRepresentation.Add(pair.Key, GetStringRepresentation(pair.Value));
            }

            string heightMap = string.Empty;

            foreach (KeyValuePair<int,string> pair in stringRepresentation)
            {
                heightMap = $"{heightMap}|{pair.Value}|";
            }

            
            for (int i = 10; i < heightMap.Length / 2; i+= 10)
            {
                string idk = new string(heightMap.TakeLast(i).ToArray());

                var temporaryHeightMap = heightMap.Replace(idk, "A");

                int occurenceCount = temporaryHeightMap.Count(x => x == 'A');

                int occurencesFromEnd = temporaryHeightMap.Reverse().TakeWhile(x => x is 'A' or '|').Count();
                
                if (occurenceCount > 1)
                {
                    //patterns.Add(new Pattern(idk, occurenceCount, occurencesFromEnd));
                }
            }

            var idk2 = patterns.OrderByDescending(x => x.OccurrenceFromEnd).First();
            
            Console.WriteLine($"Pattern of length {idk2.Representation.Length} occuring {idk2.OccurenceCount} times, {idk2.OccurrenceFromEnd} from the end");
            var temporaryHeightMap2 = heightMap.Replace(idk2.Representation, "A");
            Console.WriteLine(temporaryHeightMap2);
        }

        private static string GetStringRepresentation(IEnumerable<RockComponent> pair)
        {
            char[] representation = "        ".ToCharArray();

            foreach (RockComponent component in pair.OrderBy(x => x.CoordinateColumn))
            {
                representation[component.CoordinateColumn] = '#';
            }

            return new string(representation);
        }
    }
}