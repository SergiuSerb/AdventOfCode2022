namespace Day10.Models
{
    internal class RegistryHistoryEntry
    {
        public int Id { get; }

        public Registry Registry { get; }
        
        public int SignalStrength { get; }

        public RegistryHistoryEntry( int id, Registry registry, int signalStrength )
        {
            Id = id;
            Registry = new Registry(registry);
            SignalStrength = signalStrength;
        }
    }
}