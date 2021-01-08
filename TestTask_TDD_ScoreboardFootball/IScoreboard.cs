namespace TestTask_TDD_ScoreboardFootball
{
    public interface IScoreboard
    {
        Game StartGame(Team homeTeam, Team awayTeam);
        void RemoveGame(Game game);
    }
}
