using System;
using Day7.Interfaces;
using Day7.Models.Commands;

namespace Day7.Builders
{
    public static class CommandBuilder
    {
        private const string moveCurrentDirectoryCommandKeyword = "cd";
        private const string listCurrentDirectoryEntriesCommandKeyword = "ls";
        private const string createDirectoryCommandKeyword = "dir";

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

            if (string.Equals(keyword, moveCurrentDirectoryCommandKeyword))
            {
                return new NavigateToNestedDirectoryCommand(argument);
            }

            if (string.Equals(keyword, listCurrentDirectoryEntriesCommandKeyword))
            {
                return new ListDirectoryEntriesCommand();
            }

            if (string.Equals(keyword, createDirectoryCommandKeyword))
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