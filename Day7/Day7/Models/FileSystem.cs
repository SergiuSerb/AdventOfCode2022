namespace Day7.Models
{
    public class FileSystem
    {
        private const int MaxSize = 70000000;

        public Directory Root { get; }

        public int MaximumSize => MaxSize;

        public int CurrentUnusedSpace => MaxSize - Root.Size;

        public FileSystem(Directory root)
        {
            Root = root;
        }
    }
}