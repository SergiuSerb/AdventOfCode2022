using System.Collections.Generic;
using System.Linq;

namespace Day10.Models.Commands
{
    internal class CommandBatch
    {
        private int _currentCommandIndex;
        private readonly IList<CommandBase> _commands;

        public CommandBatch()
        {
            _commands = new List<CommandBase>();
            _currentCommandIndex = 0;
        }

        public bool IsFinished => _commands.All( x => x.IsCompleted );

        public void AddCommand( CommandBase command )
        {
            _commands.Add(command);
        }

        public CommandBase GetNext()
        {
            _currentCommandIndex++;
            
            return _commands[_currentCommandIndex - 1];
        }
    }
}