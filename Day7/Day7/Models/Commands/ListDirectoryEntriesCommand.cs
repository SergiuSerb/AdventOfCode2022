using Day7.Interfaces;

namespace Day7.Models.Commands
{
    public class ListDirectoryEntriesCommand : ICommand
    {
        public string Argument { get; }

        public void Execute(FileSystemExplorer fileSystemExplorer)
        {
            //do something
        }
    }
}