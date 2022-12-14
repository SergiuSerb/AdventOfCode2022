using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Day13.Models;

namespace Day13
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            string[] inputLines = ReadInputFile();
            IList<SequenceGroup> groups = CreateSequenceGroups(inputLines);

            DetermineCorrectGroups(groups);
        }

        private static void DetermineCorrectGroups(IList<SequenceGroup> groups)
        {
            Debug(groups);
            int correctGroupIdsSum = groups.Where(x => x.IsCorrect).Sum(x => x.Id);
            Console.WriteLine($"The IDs sum of the correct groups is {correctGroupIdsSum}.");
        }

        private static void Debug(IList<SequenceGroup> groups)
        {
            var idk = groups.Where(x => !x.IsCorrect);

            foreach (SequenceGroup sequenceGroup in idk)
            {
                //var idk2 =sequenceGroup.IsCorrect;
            }
        }

        private static IList<SequenceGroup> CreateSequenceGroups(string[] inputLines)
        {
            IList<SequenceGroup> groups = new List<SequenceGroup>();
            
            int currentSequenceGroupId = 1;
            string leftSequence = string.Empty;
            string rightSequence = string.Empty;
            
            foreach (string inputLine in inputLines)
            {
                if (string.IsNullOrWhiteSpace(inputLine))
                {
                    currentSequenceGroupId = CreateNewGroup(currentSequenceGroupId, leftSequence, rightSequence, groups);

                    leftSequence = string.Empty;
                    rightSequence = string.Empty;
                    continue;
                }

                if (string.IsNullOrWhiteSpace(leftSequence))
                {
                    leftSequence = CleanUp(inputLine);
                }
                else
                {
                    rightSequence = CleanUp(inputLine);
                }
            }
            
            CreateNewGroup(currentSequenceGroupId, leftSequence, rightSequence, groups);

            return groups;
        }

        private static string CleanUp(string inputLine)
        {
            string cleanedUpLine = string.Empty;
            string sussyString = string.Empty;

            foreach (char character in inputLine)
            {
                if (!char.IsDigit(character))
                {
                    if (!string.IsNullOrWhiteSpace(sussyString))
                    {
                        int sussyNumber = int.Parse(sussyString);
                        sussyString = string.Empty;
                        char sussyCharacter = (char)('a' + sussyNumber);
                        cleanedUpLine = $"{cleanedUpLine}{sussyCharacter}";
                    }
                    
                    
                    cleanedUpLine = $"{cleanedUpLine}{character}";
                    continue;
                }

                sussyString = $"{sussyString}{character}";
            }

            return cleanedUpLine;
        }

        private static int CreateNewGroup(int currentSequenceGroupId, string leftSequence, string rightSequence, IList<SequenceGroup> groups)
        {
            Sequence left = new Sequence($"{currentSequenceGroupId}-left", leftSequence);
            Sequence right = new Sequence($"{currentSequenceGroupId}-right", rightSequence);
            groups.Add(new SequenceGroup(currentSequenceGroupId, left, right));

            return currentSequenceGroupId + 1;
        }

        private static string[] ReadInputFile()
        {
            string? executingPath = Path.GetDirectoryName( Assembly.GetEntryAssembly()?.Location );
            const string inputFileName = "input3.txt";
            string inputPath = $"{executingPath}\\{inputFileName}";

            ReadLines( inputPath, out string[] strings );
    
            return strings;
        }

        private static void ReadLines( string s, out string[] inputLines )
        {
            try
            {
                inputLines = File.ReadAllLines( s );
            }
            catch (Exception)
            {
                throw new ArgumentNullException( "No lines could be read from input file." );
            }
        }

        private static IList<string> MatchRegex( string stringToMatch, string regexPattern)
        {
            Regex regex = new Regex( regexPattern );
            Match match = regex.Match( stringToMatch );

            IList<string> groups = new List<string>();
            
            foreach ( Group group in match.Groups) 
            {
                groups.Add(group.Value);
            }

            groups.RemoveAt(0);
            return groups;
        }
    }
}