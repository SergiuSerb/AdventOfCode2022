using System;

namespace Day7.Models
{
    public class FileSystem
    {
        public Directory Root { get; }

        public int MaxSize => 70000000;

        public int CurrentUnusedSpace => MaxSize - Root.Size;

        public FileSystem(Directory root)
        {
            Root = root;
        }
    }
}