using System;
using System.Collections.Generic;
using System.Linq;

namespace Day17.Models
{
    public class PatternSearcher
    {
        public void Search(Dictionary<int, List<RockComponent>> getLastRun)
        {
            Dictionary<int, string> stringRepresentation = new Dictionary<int, string>();
            
            foreach (KeyValuePair<int,List<RockComponent>> pair in getLastRun)
            {
                stringRepresentation.Add(pair.Key, GetStringRepresentation(pair.Value));
            }

            Dictionary<string, IEnumerable<int>> patternAppearance = stringRepresentation.GroupBy(x => x.Value).ToDictionary(x => x.Key, y => y.Select(x => x.Key));
                
            foreach (KeyValuePair<string, IEnumerable<int>> pattern in patternAppearance )
            {
                for (int period = pattern.Value.Last(); period >= pattern.Value.First(); period--)
                {
                    if (period is 0 or 1)
                    {
                        continue;
                    }
                    
                    int repetitionCount = pattern.Value.Count(x => x % period == 0);
                    
                    if (repetitionCount > 1)
                    {
                        if ( ValidatePattern(period, patternAppearance, pattern.Value.First(x => x % period == 0)))
                        {
                            Console.WriteLine($"Pattern found, repeating event {period} lines starting at {pattern.Value.First(x => x % period == 0)}!");
                            break;
                        }
                    }
                }
            }
            
        }

        private bool ValidatePattern(int period, Dictionary<string, IEnumerable<int>> patternAppearance, int firstValue)
        {
            foreach (KeyValuePair<string, IEnumerable<int>> grouping in patternAppearance)
            {
                var repetitionsAfterFirstValue = grouping.Value.Where(x => x >= firstValue);
                
                int count = repetitionsAfterFirstValue.Count(x => x % period == 0);

                if (count < 2 && repetitionsAfterFirstValue.Count() > 1)
                {
                    return false;
                }
            }

            return true;
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