// See https://aka.ms/new-console-template for more information

using System.Reflection;
using Day2.Models;

string[] inputLines = ReadInputFile();

IList<Round> rounds = CreateStrategiesWithEnemyAndFriendlyMoves(inputLines);
DetermineAllyPlayerPoints(rounds);

IList<Round> roundsWithOutcome = CreateStrategiesWithEnemyMoveAndGameOutcome(inputLines);
DetermineAllyPlayerPointsFromOutcome(roundsWithOutcome);

IList<Round> CreateStrategiesWithEnemyAndFriendlyMoves(string[] inputLines)
{
    const char inputLineSeparator = ' ';
    IList<Round> strategies = new List<Round>();
    foreach (string inputLine in inputLines)
    {
        string[] splitLine = inputLine.Split(inputLineSeparator, StringSplitOptions.TrimEntries);

        Round round = new Round(EnemyMove.GetById(splitLine[0][0]), FriendlyMove.GetById(splitLine[1][0]));

        strategies.Add(round);
    }

    return strategies;
}

IList<Round> CreateStrategiesWithEnemyMoveAndGameOutcome(string[] inputLines)
{
    const char inputLineSeparator = ' ';
    IList<Round> strategies = new List<Round>();
    foreach (string inputLine in inputLines)
    {
        string[] splitLine = inputLine.Split(inputLineSeparator, StringSplitOptions.TrimEntries);

        Round round = new Round(EnemyMove.GetById(splitLine[0][0]), GameOutcome.GetById(splitLine[1][0]));

        strategies.Add(round);
    }

    return strategies;
}

void DetermineAllyPlayerPoints(IList<Round> rounds)
{
    Player friendlyPlayer = GameCalculator.DetermineFriendlyPlayerPointsByFriendlyMove(rounds);
    Console.WriteLine($"Following the strategy, the friendly player has obtained {friendlyPlayer.Score} points.");
}

void DetermineAllyPlayerPointsFromOutcome(IList<Round> rounds)
{
    Player friendlyPlayer = GameCalculator.DetermineFriendlyPlayerPointsByGameOutcome(rounds);
    Console.WriteLine($"Following the given outcome, the friendly player has obtained {friendlyPlayer.Score} points.");
}

string[] ReadInputFile()
{
    string? executingPath = Path.GetDirectoryName( Assembly.GetEntryAssembly()?.Location );
    const string inputFileName = "input.txt";
    string inputPath = $"{executingPath}\\{inputFileName}";

    ReadLines( inputPath, out string[] strings );
    
    return strings;
}

void ReadLines( string s, out string[] inputLines )
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