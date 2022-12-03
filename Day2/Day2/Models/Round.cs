namespace Day2.Models
{
    public class Round
    {
        public EnemyMove EnemyMove { get; }

        public FriendlyMove FriendlyMove { get; }

        public GameOutcome Outcome { get; }

        public Round(EnemyMove enemyMove, FriendlyMove friendlyMove)
        {
            EnemyMove = enemyMove ?? throw new ArgumentNullException(nameof(enemyMove));
            FriendlyMove = friendlyMove ?? throw new ArgumentNullException(nameof(friendlyMove));
            Outcome = null;
        }

        public Round(EnemyMove enemyMove, GameOutcome outcome)
        {
            EnemyMove = enemyMove ?? throw new ArgumentNullException(nameof(enemyMove));
            Outcome = outcome ?? throw new ArgumentNullException(nameof(outcome));
        }
    }
}