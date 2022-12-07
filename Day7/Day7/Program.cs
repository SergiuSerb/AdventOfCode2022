using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Day7.Builders;
using Day7.Interfaces;
using Day7.Models;
using Day7.Models.Commands;
using Directory = Day7.Models.Directory;

namespace Day7
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] inputLines = ReadInputFile();

            FileSystem fileSystem = CreateFileSystem(inputLines);
            DetermineTotalSizeOfDirectoriesUnder100000(fileSystem);
            DetermineDirectoryToDelete(fileSystem);
        }

        private static void DetermineDirectoryToDelete(FileSystem fileSystem)
        {
            FileSystemExplorer explorer = new FileSystemExplorer(fileSystem);
            
            IList<Directory> uniqueDirectories = explorer.GetAllDirectories();

            int sizeOfDirectoryToDelete = uniqueDirectories.Where(x => fileSystem.CurrentUnusedSpace + x.Size >= 30000000).Min(x => x.Size);
            
            Console.WriteLine($"The size of the directory to delete is {sizeOfDirectoryToDelete}.");
        }

        private static void DetermineTotalSizeOfDirectoriesUnder100000(FileSystem fileSystem)
        {
            FileSystemExplorer explorer = new FileSystemExplorer(fileSystem);
            
            IList<Directory> uniqueDirectories = explorer.GetAllDirectories();
            IEnumerable<Directory> directoriesUnder100000 = uniqueDirectories.Where(x => x.Size <= 100000);
            int sumOfDirectoriesUnder100000 = directoriesUnder100000.Sum(x => x.Size);
            
            Console.WriteLine($"The total size of the directories under 100000 is {sumOfDirectoriesUnder100000}.");
        }

        private static FileSystem CreateFileSystem(string[] inputLines)
        {
            Directory root = new Directory("/", null);
            FileSystem fileSystem = new FileSystem(root);
            FileSystemExplorer fileExplorer = new FileSystemExplorer(fileSystem);

            foreach (string inputLine in inputLines)
            {
                if (string.IsNullOrWhiteSpace(inputLine))
                {
                    continue;
                }
                
                ICommand command = CommandBuilder.Build(inputLine);
                
                command.Execute(fileExplorer);
            }

            return fileSystem;
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
    }
}