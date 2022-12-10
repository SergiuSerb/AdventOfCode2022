namespace Day10.Models.Commands
{
    internal abstract class CommandBase
    {
        protected abstract int CyclesToComplete { get; }

        protected int CurrentCycles { get; set; }

        public bool IsCompleted => CurrentCycles == CyclesToComplete;

        public abstract void Cycle( Registry registry );

        protected abstract void PerformOperation( Registry registry );
    }
}