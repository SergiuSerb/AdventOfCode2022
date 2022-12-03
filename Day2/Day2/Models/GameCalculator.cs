namespace Day2.Models
{
    public static class GameCalculator
    {
        public static Player DetermineFriendlyPlayerPointsByFriendlyMove(IList<Round> strategies)
        {
            Player enemy = new Player("Enemy");
            Player ally = new Player("Ally");

            foreach (Round strategy in strategies)
            {
                GameRule outcome = GameRule.AllRules.First(x => x.EnemyMove == strategy.EnemyMove && x.FriendlyMove == strategy.FriendlyMove);

                if (outcome == null)
                {
                    throw new ApplicationException("Invalid move.");
                }
                
                enemy.AddScore(outcome.PointsToEnemy);
                ally.AddScore(outcome.PointToFriendly);
            }

            return ally;
        }  
        
        public static Player DetermineFriendlyPlayerPointsByGameOutcome(IList<Round> strategies)
        {
            Player enemy = new Player("Enemy");
            Player ally = new Player("Ally");

            foreach (Round strategy in strategies)
            {
                GameRule outcome = GameRule.AllRules.First(x => x.EnemyMove == strategy.EnemyMove && x.OutcomeForFriendly == strategy.Outcome);

                if (outcome == null)
                {
                    throw new ApplicationException("Invalid move.");
                }
                
                enemy.AddScore(outcome.PointsToEnemy);
                ally.AddScore(outcome.PointToFriendly);
            }

            return ally;
        }
    }
}