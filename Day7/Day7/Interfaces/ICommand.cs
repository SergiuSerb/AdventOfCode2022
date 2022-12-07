using Day7.Models;

namespace Day7.Interfaces
{
    public interface ICommand
    {
        void Execute(FileSystemExplorer fileSystemExplorer);
    }
}