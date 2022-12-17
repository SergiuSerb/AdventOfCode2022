using System.Security.Cryptography.X509Certificates;
using Day14.Events;
using Day14.Tools;

namespace Day14.Models
{
    public class Simulator
    {
        private SandSpawn _sandSpawn;
        private bool _sandSpawnReached;
        public int SandThatReachedSpawnId { get; set; }

        public Simulator( Map map )
        {
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
                
                if ( _sandSpawn.currentSandId % 100 == 0 )
                {
                    Map.DestructNonSignificantSand();
                }
            }
        }

        private void CreateSandSpawn( Map map )
        {
            _sandSpawn = new SandSpawn( 0, 500 );
            map.AddSandSpawn( _sandSpawn );
        }
    }
}