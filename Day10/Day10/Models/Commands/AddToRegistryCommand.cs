using System;

namespace Day10.Models.Commands
{
    internal class AddToRegistryCommand : CommandBase
    {
        public static string Keyword => "addx";
        
        private int Argument { get; }

        protected override int CyclesToComplete => 2;

        public AddToRegistryCommand( int argument )
        {
            CurrentCycles = 0;
            Argument = argument;
        }

        public override void Cycle(Registry registry)
        {
            
            CurrentCycles++;

            if ( IsCompleted )
            {
                PerformOperation(registry);
            }
        }

        protected override void PerformOperation( Registry registry )
        {
            registry.X += Argument;
        }
    }
}