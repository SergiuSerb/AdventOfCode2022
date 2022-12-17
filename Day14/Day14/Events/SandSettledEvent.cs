using Day14.Models;
using Day14.Tools.EventAggregator;

namespace Day14.Events
{
    public class SandSettledEvent : IEvent
    {
        public Sand Sand { get; }

        public SandSettledEvent( Sand sand )
        {
            Sand = sand;
        }
    }
}