using System.Reflection;
using Day1.Models;

string executingPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
string inputFileName = "input.txt";
string inputPath = $"{executingPath}\\{inputFileName}";

ReadLines( inputPath, out string[] inputLines );
CreateElves( inputLines, out IList<Elf> elves );
elves = elves.OrderBy( x => x.CurrentlyCarriedCalories ).ToList();

DetermineTopElf( elves );
DetermineTopElves( elves, 3 );

void CreateElves( string[] inputLines, out IList<Elf> elves )
{
    if ( inputLines.Length == 0 )
    {
        throw new ArgumentException( "No elves could be created." );
    }

    elves = new List<Elf>();
    int currentElfId = 1;
    int currentItemId = 1;
    Elf currentElf = new Elf( currentElfId );
    foreach ( string inputLine in inputLines )
    {
        bool isParseSuccessful = Int32.TryParse( inputLine, out int itemCalories );

        if ( !isParseSuccessful )
        {
            elves.Add(currentElf);
            currentElfId++;
            currentElf = new Elf( currentElfId );
        }
        
        currentElf.PickItem(new Item(currentItemId++, itemCalories));
    }
    
    elves.Add(currentElf);
}

void ReadLines( string s, out string[] inputLines )
{
    try
    {
        inputLines = System.IO.File.ReadAllLines( s );
    }
    catch ( Exception e )
    {
        throw new ArgumentNullException( "No lines could be read from input file." );
    }
}

void DetermineTopElves( IList<Elf> list, int topCount )
{
    IEnumerable<Elf> topElves = list.TakeLast( 3 );
    int totalCaloriesAmongTop = topElves.Sum( x => x.CurrentlyCarriedCalories );

    Console.WriteLine( $"The top {topCount} elves are carrying a total of {totalCaloriesAmongTop} calories." );
}

void DetermineTopElf( IList<Elf> elves )
{
    Elf elfCarryingMaxCalories = elves.Last();
    Console.WriteLine( $"The elf with ID {elfCarryingMaxCalories.Id} is carrying the most calories, {elfCarryingMaxCalories.CurrentlyCarriedCalories}." );
}