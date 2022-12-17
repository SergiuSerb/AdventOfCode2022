using Day14.Events;
using Day14.Tools;

namespace Day14.Models
{
    public class SandSpawn : IPlaceable
    {
        public int CoordinatesRow { get; set; }

        public int CoordinatesColumn { get; set; }

        public int currentSandId;

        public SandSpawn(int coordinatesRow, int coordinatesColumn)
        {
            CoordinatesRow = coordinatesRow;
            CoordinatesColumn = coordinatesColumn;
            currentSandId = 1;
            
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            EventAggregator.Subscribe<SandSettledEvent>( this, OnSandSettled );
        }

        private void OnSandSettled( SandSettledEvent sandSettledEvent )
        {
            SettledSand settledSand = new SettledSand(sandSettledEvent.Sand.Id, sandSettledEvent.Sand.CoordinatesRow, sandSettledEvent.Sand.CoordinatesColumn);
            Map.Items.Add(settledSand);

            if ( sandSettledEvent.Sand.CoordinatesRow == CoordinatesRow && sandSettledEvent.Sand.CoordinatesColumn == CoordinatesColumn )
            {
                EventAggregator.Publish(new SandSpawnReachedEvent());
            }
        }

        public void Spawn()
        {
            Sand sand = new Sand(currentSandId++);
            
            sand.MoveTo(CoordinatesRow, CoordinatesColumn);

            EventAggregator.Publish(new SandSettledEvent(sand));
        }
    }
}