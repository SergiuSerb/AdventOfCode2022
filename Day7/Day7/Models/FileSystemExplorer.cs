using System;
using System.Collections.Generic;
using System.Linq;
using Day7.Interfaces;

namespace Day7.Models
{
    public class FileSystemExplorer
    {
        public FileSystem FileSystem { get; }

        public Directory CurrentDirectory { get; set; }

        public FileSystemExplorer(FileSystem fileSystem)
        {
            FileSystem = fileSystem;
            CurrentDirectory = FileSystem.Root;
        }

        public void NavigateToDirectoryByName(string argument)
        {
            IFileSystemEntry entryToMoveTo;
            if (string.Equals(argument, ".."))
            {
                entryToMoveTo = CurrentDirectory.Parent;

            }
            else
            {
                entryToMoveTo = CurrentDirectory.GetEntryByName(argument);
            }
            
            if (entryToMoveTo == null)
            {
                entryToMoveTo = CurrentDirectory;
            }

            if (entryToMoveTo is not Directory entryAsDirectory)
            {
                throw new ApplicationException("Cannot navigate to file of filetype other than directory.");
            }

            CurrentDirectory = entryAsDirectory;
        }

        public IList<Directory> GetAllDirectories()
        {
            List<Directory> nestedDirectories = CurrentDirectory.GetNestedDirectories();
            
            int currentDirectoryIndex = 0;
            while (currentDirectoryIndex < nestedDirectories.Count)
            {
                Directory currentDirectory = nestedDirectories[currentDirectoryIndex];
                nestedDirectories.AddRange(currentDirectory.GetNestedDirectories());
                currentDirectoryIndex++;
            }
            
            IEnumerable<IGrouping<string, Directory>> groupedDirectories = nestedDirectories.GroupBy(x => x.Id);
            List<Directory> uniqueDirectories = groupedDirectories.Select(x => x.First()).ToList();

            return uniqueDirectories;
        }
    }
}