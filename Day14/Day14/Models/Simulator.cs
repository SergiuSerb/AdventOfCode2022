using Day14.Events;
using Day14.Tools.EventAggregator;

namespace Day14.Models
{
    public class Simulator
    {
        private readonly bool _selfManagePlaceables;
        private SandSpawn _sandSpawn;
        private bool _sandSpawnReached;
        
        public int SandThatReachedSpawnId { get; private set; }

        public Simulator( Map map, bool selfManagePlaceables )
        {
            _selfManagePlaceables = selfManagePlaceables;
            CreateSandSpawn( map );
            SubscribeToEvents();

            _sandSpawnReached = false;
        }

        private void SubscribeToEvents()
        {
            EventAggregator.Subscribe<SandSpawnReachedEvent>(this, Handler);
        }

        private void Handler( SandSpawnReachedEvent sandSpawnReachedEvent )
        {
            _sandSpawnReached = true;
            SandThatReachedSpawnId = _sandSpawn.currentSandId;
        }

        public void RunSimulation()
        {
            while ( !_sandSpawnReached )
            {
                _sandSpawn.Spawn();
                
                if ( _selfManagePlaceables && _sandSpawn.currentSandId % 100 == 0 )
                {
                    Map.DestructNonSignificantPlaceables();
                }
            }
        }

        private void CreateSandSpawn( Map map )
        {
            _sandSpawn = new SandSpawn( 0, 500 );
            Map.AddSandSpawn( _sandSpawn );
        }
    }
}