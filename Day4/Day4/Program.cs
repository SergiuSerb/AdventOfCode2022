using System.Reflection;
using Day4.Models;

string[] inputLines = ReadInputFile();

IList<ElfPair> pairs = CreateElfPairs( inputLines );
DetermineNumberOfDuplicatedPairs( pairs );
DetermineNumberOfOverlappingPairs( pairs );

void DetermineNumberOfDuplicatedPairs( IList<ElfPair> elfPairs )
{
    int duplicatedCount = elfPairs.Count( x => x.IsFullyDuplicated() );
    Console.WriteLine($"The number of duplicated groups is {duplicatedCount}.");
}

void DetermineNumberOfOverlappingPairs( IList<ElfPair> elfPairs )
{
    int overlappingCount = elfPairs.Count( x => x.IsOverlapping() );
    Console.WriteLine($"The number of overlapping groups is {overlappingCount}.");
}

IList<ElfPair> CreateElfPairs( string[] inputLines )
{
    IList<ElfPair> elfPairs = new List<ElfPair>();

    var currentElfId = 1;
    foreach ( string line in inputLines )
    {
        const char separator = ',';
        string[] separatedLine = line.Split( separator );
        Elf firstElf = CreateElf( currentElfId++, separatedLine[0] );
        Elf secondElf = CreateElf( currentElfId++, separatedLine[1] );
        
        elfPairs.Add( new ElfPair(firstElf, secondElf));
    }

    return elfPairs;
}

Elf CreateElf( int elfId, string inputLine )
{
    var elf = new Elf( elfId );
    const char separator = '-';
    string[] separatedElfLine = inputLine.Split( separator );

    int startSection = int.Parse( separatedElfLine[0] );
    int endSection = int.Parse( separatedElfLine[1] );

    for ( int sectionId = startSection; sectionId <= endSection; sectionId++ )
    {
        var section = new Section( sectionId );
        elf.AddSection(section);
    }

    return elf;
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