namespace Day10.Models.Commands
{
    internal class NoOperationCommandBase : CommandBase
    {
        public static string Keyword => "noop";

        protected override int CyclesToComplete => 1;

        public NoOperationCommandBase()
        {
            CurrentCycles = 0;
        }
        
        public override void Cycle( Registry registry )
        {
            CurrentCycles++;
            
            if ( IsCompleted )
            {
                PerformOperation(registry);
            }
        }

        protected override void PerformOperation( Registry registry )
        {
            //nothing
        }
    }
}