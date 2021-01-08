using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask_TDD_ScoreboardFootball
{
    public class Scoreboard : IScoreboard
    {
        private readonly List<Game> games = new List<Game>();

        public IEnumerable<Game> GetGames()
        {
            foreach (var game in games)
                yield return game;
        }

        public void RemoveGame(Game game)
        {
            if (!games.Contains(game))
                throw new ArgumentException("Game is not started or already finished.");

            games.Remove(game);
        }


        public Game StartGame(Team homeTeam, Team awayTeam)
        {
            if (homeTeam == null || awayTeam == null)
                throw new ArgumentNullException();

            if (games.Any(x => 
                x.HomeTeam.Equals(homeTeam) || x.HomeTeam.Equals(awayTeam) ||
                x.AwayTeam.Equals(homeTeam) || x.AwayTeam.Equals(awayTeam)))
            {
                throw new ArgumentException("One of teams already play game.");
            }

            var game = new Game(homeTeam, awayTeam, this);
            games.Add(game);

            return game;
        }

        public string GetSummary()
        {
            var sortedGames = games
                .OrderByDescending(x => x.HomeTeamPoints + x.AwayTeamPoints)
                .ThenBy(x => x.DateGameStarted);

            var sb = new StringBuilder();
            foreach (var game in sortedGames)
            {
                sb.AppendLine(game.ToString());
            }

            return sb.ToString();
        }
    }
}
