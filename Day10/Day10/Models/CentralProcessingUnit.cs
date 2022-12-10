using System.Collections.Generic;
using System.Linq;
using Day10.Models.Commands;

namespace Day10.Models
{
    internal class CentralProcessingUnit
    {
        private readonly GraphicalProcessingUnit _graphicalProcessingUnit;

        public int CurrentCycle { get; set; }
        
        public CommandBase CurrentRunningCommand { get; set; }

        public int CurrentSignalStrength => CurrentCycle * _registry.X;

        private IList<RegistryHistoryEntry> _registryHistory;
        private readonly Registry _registry;
        private CommandBatch _commandBatch;
        
        public CentralProcessingUnit(GraphicalProcessingUnit graphicalProcessingUnit)
        {
            _graphicalProcessingUnit = graphicalProcessingUnit;
            CurrentCycle = 1;
            _registry = new Registry( 1 );
            _registryHistory = new List<RegistryHistoryEntry>();
        }

        public void RunBatch( CommandBatch batch )
        {
            _commandBatch = batch;
            
            CurrentRunningCommand = _commandBatch.GetNext();
            while ( !_commandBatch.IsFinished )
            {
                Cycle();
            }
        }

        private void Cycle()
        {
            LogRegistry();
            _graphicalProcessingUnit.Cycle( CurrentCycle, _registry );
            
            CurrentCycle++;
            if ( CurrentRunningCommand.IsCompleted )
            {
                CurrentRunningCommand = _commandBatch.GetNext();
            }
            
            CurrentRunningCommand.Cycle(_registry);
        }

        private void LogRegistry()
        {
            _registryHistory.Add( new RegistryHistoryEntry( CurrentCycle, _registry, CurrentSignalStrength ) );
        }

        public int GetRegistrySignalStrengthSumAtIndices( IList<int> indexes )
        {
            int registrySum = 0;
            foreach ( int index in indexes )
            {
                registrySum += _registryHistory.First( x => x.Id == index ).SignalStrength;
            }

            return registrySum;
        }

        public void PrintGpuCanvas()
        {
            _graphicalProcessingUnit.PrintCanvas();
        }
    }
}