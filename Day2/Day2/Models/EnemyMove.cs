namespace Day2.Models
{
    public class EnemyMove : Move
    {
        private EnemyMove(char id, string name, int score) : base(id, name, score)
        {
            
        }

        public static EnemyMove Rock = new EnemyMove('A', nameof(Rock), 1);
        public static EnemyMove Paper = new EnemyMove('B', nameof(Paper), 2);
        public static EnemyMove Scissors = new EnemyMove('C', nameof(Scissors), 3);

        private static readonly IList<EnemyMove> All = new List<EnemyMove>()
        {
            Rock, Paper, Scissors
        };

        public override string ToString()
        {
            return Name;
        }

        public static EnemyMove GetById(char id)
        {
            EnemyMove move = All.FirstOrDefault(move => move.Id == id);

            if (move == null)
            {
                throw new ApplicationException("Invalid move.");
            }

            return move;
        }
    }
}