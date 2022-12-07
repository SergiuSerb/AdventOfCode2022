using Day7.Interfaces;
using Day7.Models.Commands;

namespace Day7.Builders
{
    public static class CommandBuilder
    {
        private const string MoveCurrentDirectoryCommandKeyword = "cd";
        private const string ListCurrentDirectoryEntriesCommandKeyword = "ls";
        private const string CreateDirectoryCommandKeyword = "dir";

        public static ICommand Build(string inputLine)
        {
            string cleanedUpLine = CleanUpInputLine(inputLine);
            string[] commandParts = cleanedUpLine.Split(' ');

            string keyword = commandParts[0];
            string argument = string.Empty;

            if (commandParts.Length > 1)
            {
                argument = commandParts[1];
            }

            if (string.Equals(keyword, MoveCurrentDirectoryCommandKeyword))
            {
                return new NavigateToNestedDirectoryCommand(argument);
            }

            if (string.Equals(keyword, ListCurrentDirectoryEntriesCommandKeyword))
            {
                return new ListDirectoryEntriesCommand();
            }

            if (string.Equals(keyword, CreateDirectoryCommandKeyword))
            {
                return new CreateDirectoryEntryCommand(argument);
            }
            
            return new CreateDocumentEntryCommand(argument, int.Parse(keyword));
        }

        private static string CleanUpInputLine(string inputLine)    
        {
            int indexOfBeginCommandToken = inputLine.IndexOf('$');

            if (indexOfBeginCommandToken < 0)
            {
                return inputLine;
            }
            
            return inputLine.Remove(indexOfBeginCommandToken, 1).Trim();
        }
    }
}