namespace Day7.Interfaces
{
    public interface IFileSystemEntry
    {
        public int Size { get; }

        public string Name { get; }

        public string Id { get; }
    }
}