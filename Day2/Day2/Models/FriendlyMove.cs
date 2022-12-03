namespace Day2.Models
{
    public class FriendlyMove : Move
    {
        private FriendlyMove(char id, string name) : base(id, name)
        {
            
        }

        public static FriendlyMove Rock = new FriendlyMove('X', nameof(Rock));
        public static FriendlyMove Paper = new FriendlyMove('Y', nameof(Paper));
        public static FriendlyMove Scissors = new FriendlyMove('Z', nameof(Scissors));

        private static readonly IList<FriendlyMove> All = new List<FriendlyMove>()
        {
            Rock, Paper, Scissors
        };
        
        public static FriendlyMove GetById(char id)
        {
            FriendlyMove? move = All.FirstOrDefault(move => move.Id == id);

            if (move == null)
            {
                throw new ApplicationException("Invalid move.");
            }

            return move;
        }
    }
}