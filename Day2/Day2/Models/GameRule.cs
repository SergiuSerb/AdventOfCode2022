namespace Day2.Models
{
    public class GameRule
    {
        public EnemyMove EnemyMove { get; }

        public FriendlyMove FriendlyMove { get; }
        
        public GameOutcome OutcomeForFriendly { get; }

        public int PointsToEnemy { get; }

        public int PointToFriendly { get; }

        public GameRule(EnemyMove enemyMove, FriendlyMove friendlyMove, GameOutcome outcomeForFriendly, int pointsToEnemy, int pointToFriendly)
        {
            EnemyMove = enemyMove;
            FriendlyMove = friendlyMove;
            OutcomeForFriendly = outcomeForFriendly;
            PointsToEnemy = pointsToEnemy;
            PointToFriendly = pointToFriendly;
        }

        public static IList<GameRule> AllRules = new List<GameRule>()
        {
            new GameRule(EnemyMove.Rock, FriendlyMove.Rock, GameOutcome.Draw , DrawPoints + 1, DrawPoints + 1),
            new GameRule(EnemyMove.Rock, FriendlyMove.Paper, GameOutcome.Victory , DefeatPoints + 1, VictoryPoints + 2),
            new GameRule(EnemyMove.Rock, FriendlyMove.Scissors, GameOutcome.Defeat , VictoryPoints + 1, DefeatPoints + 3),
            new GameRule(EnemyMove.Paper, FriendlyMove.Rock, GameOutcome.Defeat , VictoryPoints + 2, DefeatPoints + 1),
            new GameRule(EnemyMove.Paper, FriendlyMove.Paper, GameOutcome.Draw , DrawPoints + 2, DrawPoints + 2),
            new GameRule(EnemyMove.Paper, FriendlyMove.Scissors, GameOutcome.Victory , DefeatPoints + 2, VictoryPoints + 3),
            new GameRule(EnemyMove.Scissors, FriendlyMove.Rock, GameOutcome.Victory , DefeatPoints + 3, VictoryPoints + 1),
            new GameRule(EnemyMove.Scissors, FriendlyMove.Paper, GameOutcome.Defeat , VictoryPoints + 3, DefeatPoints + 2),
            new GameRule(EnemyMove.Scissors, FriendlyMove.Scissors, GameOutcome.Draw , DrawPoints + 3, DrawPoints + 3)
        };

        private const int VictoryPoints = 6;
        private const int DrawPoints = 3;
        private const int DefeatPoints = 0;
    }
}