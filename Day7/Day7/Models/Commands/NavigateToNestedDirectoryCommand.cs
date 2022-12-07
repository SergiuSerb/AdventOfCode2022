using Day7.Interfaces;

namespace Day7.Models.Commands
{
    public class NavigateToNestedDirectoryCommand : ICommand
    {
        private string Argument { get; }

        public NavigateToNestedDirectoryCommand(string argument)
        {
            Argument = argument;
        }
        
        public void Execute(FileSystemExplorer fileSystemExplorer)
        {
            fileSystemExplorer.NavigateToDirectoryByName(Argument);
        }
        
    }
}