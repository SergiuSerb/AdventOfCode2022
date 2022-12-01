using System.Reflection;
using Day1.Models;

string[] inputLines = ReadInputFile();
IList<Elf> elves = CreateElves( inputLines );
DetermineSolutions( elves );

IList<Elf> CreateElves( string[] inputLines )
{
    if ( inputLines.Length == 0 )
    {
        throw new ArgumentException( "No elves could be created." );
    }

    IList<Elf> elves = new List<Elf>();

    var currentElfId = 1;
    var currentItemId = 1;
    var currentElf = new Elf( currentElfId );
    
    foreach ( string inputLine in inputLines )
    {
        bool isParseSuccessful = int.TryParse( inputLine, out int itemCalories );

        if ( !isParseSuccessful )
        {
            elves.Add(currentElf);
            currentElfId++;
            currentElf = new Elf( currentElfId );
        }
        
        currentElf.PickItem(new Item(currentItemId++, itemCalories));
    }
    
    elves.Add(currentElf);

    return elves;
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

string[] ReadInputFile()
{
    string? executingPath = Path.GetDirectoryName( Assembly.GetEntryAssembly()?.Location );
    string inputFileName = "input.txt";
    string inputPath = $"{executingPath}\\{inputFileName}";

    ReadLines( inputPath, out string[] strings );
    
    return strings;
}

void DetermineSolutions( IList<Elf> elves )
{
    void DetermineTopElf( IList<Elf> elves )
    {
        Elf elfCarryingMaxCalories = elves.Last();
        Console.WriteLine( $"The elf with ID {elfCarryingMaxCalories.Id} is carrying the most calories, {elfCarryingMaxCalories.CurrentlyCarriedCalories}." );
    }

    void DetermineTopElves( IList<Elf> list, int topCount )
    {
        IEnumerable<Elf> topElves = list.TakeLast( 3 );
        int totalCaloriesAmongTop = topElves.Sum( x => x.CurrentlyCarriedCalories );

        Console.WriteLine( $"The top {topCount} elves are carrying a total of {totalCaloriesAmongTop} calories." );
    }

    {
        elves = elves.OrderBy( x => x.CurrentlyCarriedCalories ).ToList();

        DetermineTopElf( elves );
        DetermineTopElves( elves, 3 );
    }
}