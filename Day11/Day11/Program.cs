using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Day11.Models;
using Day11.Models.Operations;
using Day11.Models.Tests;

namespace Day11
{
    static class Program
    {
        private static void Main( string[] args )
        {
            List<string> inputLines = ReadInputFile().ToList();
            IList<Monkey> monkeys = CreateMonkeys(new List<string>(inputLines));
            BuildMonkeyRelationships( monkeys, inputLines );
            
            RunSimulation( monkeys, 20 );
            DetermineMonkeyBusiness( monkeys, 20 );

            inputLines = ReadInputFile().ToList();
            monkeys = CreateMonkeys(new List<string>(inputLines));
            BuildMonkeyRelationships( monkeys, inputLines );
            UpdateMonkeys( monkeys );
            RunSimulation( monkeys, 10000   );
            DetermineMonkeyBusiness( monkeys, 10000   );
        }

        private static void UpdateMonkeys( IList<Monkey> monkeys )
        {
            foreach ( Monkey monkey in monkeys )
            {
                monkey.BeforeTestOperation = null;
                monkey.SelfManageWorryLevels = true;
            }
        }

        private static void DetermineMonkeyBusiness( IList<Monkey> monkeys, int roundsCount )
        {
            IList<Monkey> mostActiveMonkeys = monkeys.OrderByDescending( x => x.InspectionCount ).Take( 2 ).ToList();
            ulong monkeyBusiness = (ulong)mostActiveMonkeys[0].InspectionCount * (ulong)mostActiveMonkeys[1].InspectionCount;
            
            Console.WriteLine($"The level of monkey business after {roundsCount} rounds is {monkeyBusiness}.");
        }

        private static void RunSimulation( IList<Monkey> monkeys, int rounds )
        {
            ulong lowestCommonMultiple = 1;

            foreach ( var divisor in monkeys.Select( x => x.Test ).Select( x => x.rightOperand ) )
            {
                lowestCommonMultiple *= divisor;
            }

            for ( int roundId = 1; roundId <= rounds; roundId++ )
            {
                foreach ( Monkey monkey in monkeys )
                {
                    monkey.DoTheMonkeyThing(lowestCommonMultiple);
                }
            }
        }

        private static void BuildMonkeyRelationships( IList<Monkey> monkeys, List<string> inputLines )
        {
            while ( inputLines.Count > 0 )
            {
                IList<string> inputGroup = GetNextMonkeyDetails( inputLines );
                IList<string> monkeyDetails = ExtractMonkeyDetailsFromInput( inputGroup );
                
                int monkeyId = int.Parse( monkeyDetails[0] );
                int monkeyIfTrueId = int.Parse( monkeyDetails[5] );
                int monkeyIfFalseId = int.Parse( monkeyDetails[6] );

                monkeys.First( x => x.Id == monkeyId ).MonkeyIfTestSucceeds =
                    monkeys.First( x => x.Id == monkeyIfTrueId );
                
                monkeys.First( x => x.Id == monkeyId ).MonkeyIfTestFails =
                    monkeys.First( x => x.Id == monkeyIfFalseId );
            }
        }

        private static IList<Monkey> CreateMonkeys( List<string> inputLines )
        {
            int itemId = 0;
            IList<Monkey> monkeys = new List<Monkey>();
            
            while ( inputLines.Count > 0 )
            {
                IList<string> inputGroup = GetNextMonkeyDetails( inputLines );
                IList<string> monkeyDetails = ExtractMonkeyDetailsFromInput( inputGroup );

                int monkeyId = int.Parse( monkeyDetails[0] );
                
                OperationBase operation = CreateOperation( monkeyDetails );
                OperationBase operationBeforeTest = new Division( 3 );
                Test test = CreateTest( monkeyDetails );

                Monkey monkey = new Monkey( monkeyId, operation, operationBeforeTest, test );

                string[] items = monkeyDetails[1].Split( ", " );
                foreach ( string itemImportance in items )
                {
                    monkey.ReceiveItem( new Item(itemId++, ulong.Parse(itemImportance)));
                }
                
                monkeys.Add(monkey);
            }

            return monkeys;
        }

        private static Test CreateTest( IList<string> inputGroup )
        {
            ulong rightOperand = ulong.Parse( inputGroup[4] );
            
            return new Test(rightOperand);
        }

        private static OperationBase CreateOperation( IList<string> inputGroup )
        {
            if ( string.Equals( inputGroup[2], Multiplication.Representation ) && string.Equals( inputGroup[3], "old" ) )
            {
                return new Square( 0 );
            }
            
            ulong rightOperand = ulong.Parse( inputGroup[3] );
            if ( string.Equals( inputGroup[2], Multiplication.Representation ) )
            {
                return new Multiplication( rightOperand );
            }

            return new Addition( rightOperand );
        }

        private static IList<string> GetNextMonkeyDetails( List<string> inputLines )
        {
            IList<string> inputGroup = inputLines.TakeWhile( x => !string.IsNullOrWhiteSpace( x ) ).ToList();

            foreach ( string inputLine in inputGroup )
            {
                inputLines.Remove( inputLine );
            }
            
            inputLines.Remove( string.Empty );

            return inputGroup;
        }

        private static IList<string> ExtractMonkeyDetailsFromInput( IList<string> inputGroup )
        {
            string groupString = string.Empty;

            foreach ( string groupLine in inputGroup )
            {
                groupString = $"{groupString}{groupLine}";
            }

            return MatchRegex( groupString,
                              @"Monkey (.*):  Starting items: (.*)  Operation: new = old (.*) (.*)  Test: divisible by (.*)    If true: throw to monkey (.*)    If false: throw to monkey (.*)" );
        }

        private static string[] ReadInputFile()
        {
            string? executingPath = Path.GetDirectoryName( Assembly.GetEntryAssembly()?.Location );
            const string inputFileName = "input.txt";
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