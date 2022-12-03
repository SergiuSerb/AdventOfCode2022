namespace Day2.Models
{
    public class GameOutcome
    {
        public char Id { get; }
        public string Name { get; }

        private GameOutcome(char id, string name)
        {
            Id = id;
            Name = name;
        }

        public static GameOutcome Victory = new GameOutcome('Z', nameof(Victory));
        public static GameOutcome Draw = new GameOutcome('Y', nameof(Draw));
        public static GameOutcome Defeat = new GameOutcome('X', nameof(Defeat));

        private static readonly IList<GameOutcome> All = new List<GameOutcome>()
        {
            Victory, Defeat, Draw
        };

        public static GameOutcome GetById(char id)
        {
            GameOutcome outcome = All.FirstOrDefault(x => x.Id == id);

            if (outcome == null)
            {
                throw new ApplicationException("Invalid outcome.");
            }

            return outcome;
        }


    }
}