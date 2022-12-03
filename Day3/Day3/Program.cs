// See https://aka.ms/new-console-template for more information

using System.Reflection;
using Day3.Models;

string[] inputLines = ReadInputFile();

IList<Elf> elves = CreateElves(inputLines);
DeterminePrioritySumOfMisplacedItems(elves); // solution 1

IList<ElfGroup> elfGroups = CreateElfGroups(elves);
DeterminePrioritySumOfGroupBadgeItems(elfGroups); // solution 2

IList<Elf> CreateElves(string[] inputLines)
{
    List<Elf> elves = new List<Elf>();
    int currentElfId = 1;
    int currentItemId = 1;
    
    foreach (string inputLine in inputLines)
    {
        Elf currentElf = new Elf(currentElfId);
        int lineHalfCount = inputLine.Length / 2;
        
        var firstHalfItems = inputLine.Take(lineHalfCount);
        foreach (char itemRepresentation in firstHalfItems)
        {
            currentElf.AddItemToRucksackFirstCompartment(new Item(currentItemId, itemRepresentation));
            currentItemId++;
        }

        var secondHalfItems = inputLine.TakeLast(lineHalfCount);
        foreach (char itemRepresentation in secondHalfItems)
        {
            currentElf.AddItemToRucksackSecondCompartment(new Item(currentItemId, itemRepresentation));
            currentItemId++;
        }
        
        elves.Add(currentElf);
        currentElfId++;
    }

    return elves;
}

void DeterminePrioritySumOfMisplacedItems(IList<Elf> elves)
{
    int commonItemPrioritySum = elves.Sum(x => x.GetMisplacedItem().Priority);

    Console.WriteLine($"The sum or common item priorities is {commonItemPrioritySum}.");
}

IList<ElfGroup> CreateElfGroups(IList<Elf> elves)
{
    IList<ElfGroup> elfGroups = new List<ElfGroup>();

    int currentElfGroupId = 1;
    ElfGroup currentElfGroup = new ElfGroup(currentElfGroupId);
    foreach (Elf elf in elves)
    {
        if (currentElfGroup.IsFull())
        {
            elfGroups.Add(currentElfGroup);
            currentElfGroupId++;
            currentElfGroup = new ElfGroup(currentElfGroupId);
        }
        
        currentElfGroup.AddToGroup(elf);
    }

    elfGroups.Add(currentElfGroup);

    return elfGroups;
}

void DeterminePrioritySumOfGroupBadgeItems(IList<ElfGroup> elfGroups)
{
    int badgeItemSum = elfGroups.Sum(x => x.GetBadgeItem().Priority);
    
    Console.WriteLine($"The sum of group badge items is {badgeItemSum}.");
}

string[] ReadInputFile()
{
    string? executingPath = Path.GetDirectoryName( Assembly.GetEntryAssembly()?.Location );
    string inputFileName = "input.txt";
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