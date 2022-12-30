using System;
using System.Linq;

namespace Day17.Models
{
    public class LogEntry
    {
        public ulong RockId { get; }
        
        public Type RockType { get; }

        public int DeltaRow { get; set; }

        public LogEntry(ulong rockId, Type rockType)
        {
            RockId = rockId;
            RockType = rockType;
            DeltaRow = 0;
        }
        
        public void DecrementDeltaRow()
        {
            DeltaRow--;
        }

        public override string ToString()
        {
            return $"{RockType.Name.First()}{DeltaRow}";
        }
    }
}