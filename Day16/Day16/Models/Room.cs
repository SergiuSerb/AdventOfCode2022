using System.Collections.Generic;

namespace Day16.Models
{
    public class Room
    {
        public string Id => Valve.Id;
        
        public Valve Valve { get; }
        
        public IList<Room> Connections { get; }

        public Room( Valve valve )
        {
            Valve = valve;
            Connections = new List<Room>();
        }

        public void AddConnection(Room connection)
        {
            Connections.Add( connection );
        }
    }
}