namespace Day16.Models
{
    public class Valve
    {
        public string Id { get; }

        public int FlowRate { get; }

        public bool IsOpen { get; private set; }

        public Valve( string id, int flowRate )
        {
            Id = id;
            FlowRate = flowRate;
            IsOpen = false;
        }

        public void Open()
        {
            IsOpen = true;
        }
    }
}