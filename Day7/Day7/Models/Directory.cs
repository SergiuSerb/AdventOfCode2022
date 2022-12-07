using System.Collections.Generic;
using System.Linq;
using Day7.Interfaces;

namespace Day7.Models
{
    public class Directory : IFileSystemEntry
    {
        private readonly IList<IFileSystemEntry> _entries;
        public Directory Parent { get; }
        
        public int Size => GetEntriesSize();
        
        public string Name { get; }

        public string Id => $"{Parent?.Id}/{Name}";

        public Directory(string name, Directory parent)
        {
            Name = name;
            Parent = parent;
            _entries = new List<IFileSystemEntry>();
        }

        public void AddEntry(IFileSystemEntry entry)
        {
            _entries.Add(entry);
        }

        public IFileSystemEntry GetEntryByName(string name)
        {
            return _entries.FirstOrDefault(x => string.Equals(x.Name, name));
        }

        public List<Directory> GetNestedDirectories()
        {
            return _entries.OfType<Directory>().ToList();
        }

        private int GetEntriesSize()
        {
            return _entries.Sum(x => x.Size);
        }
    }
}