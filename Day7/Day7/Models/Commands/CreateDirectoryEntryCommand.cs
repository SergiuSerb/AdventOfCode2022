using Day7.Interfaces;

namespace Day7.Models.Commands
{
    public class CreateDirectoryEntryCommand : ICommand
    {
        private string DirectoryName { get; }

        public CreateDirectoryEntryCommand(string directoryName)
        {
            DirectoryName = directoryName;
        }

        public void Execute(FileSystemExplorer fileSystemExplorer)
        {
            Directory directory = new Directory(DirectoryName, fileSystemExplorer.CurrentDirectory);
            fileSystemExplorer.CurrentDirectory.AddEntry(directory);
        }
    }
}