using Day7.Interfaces;

namespace Day7.Models
{
    public class Document : IFileSystemEntry
    {
        private int _size;

        public int Size => _size;

        public string Name { get; }

        public string Id => Name;

        public Document(int size, string name)
        {
            _size = size;
            Name = name;
        }
    }
}