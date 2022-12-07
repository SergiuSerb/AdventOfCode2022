using Day7.Interfaces;

namespace Day7.Models
{
    public class Document : IFileSystemEntry
    {
        public int Size { get; }

        public string Name { get; }

        public string Id => Name;

        public Document(int size, string name)
        {
            Size = size;
            Name = name;
        }
    }
}