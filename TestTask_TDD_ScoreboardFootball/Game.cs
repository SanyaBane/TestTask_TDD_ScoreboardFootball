using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask_TDD_ScoreboardFootball
{
    public class Game
    {
        public Team HomeTeam { get; }
        public Team AwayTeam { get; }

        private readonly Scoreboard scoreboard;

        public ushort HomeTeamPoints { get; private set; }
        public ushort AwayTeamPoints { get; private set; }

        public DateTime DateGameStarted { get; }

        public Game(Team homeTeam, Team awayTeam, Scoreboard scoreboard)
        {
            if (homeTeam.Equals(awayTeam))
                throw new ArgumentException("Game can not be created for two same teams.");

            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
            this.scoreboard = scoreboard;

            DateGameStarted = DateTime.Now;
        }

        public void UpdateScore(ushort homeTeamPoints, ushort awayTeamPoints)
        {
            HomeTeamPoints = homeTeamPoints;
            AwayTeamPoints = awayTeamPoints;
        }

        public void FinishGame()
        {
            scoreboard.RemoveGame(this);
        }

        public override string ToString()
        {
            return $"{HomeTeam.Title} - {AwayTeam.Title}: {HomeTeamPoints} - {AwayTeamPoints}";
        }

        #region Equals and GetHashCode
        public override bool Equals(object obj)
        {
            return obj is Game game &&
                   EqualityComparer<Team>.Default.Equals(HomeTeam, game.HomeTeam) &&
                   EqualityComparer<Team>.Default.Equals(AwayTeam, game.AwayTeam);
        }

        public override int GetHashCode()
        {
            int hashCode = 1416656145;
            hashCode = hashCode * -1521134295 + EqualityComparer<Team>.Default.GetHashCode(HomeTeam);
            hashCode = hashCode * -1521134295 + EqualityComparer<Team>.Default.GetHashCode(AwayTeam);
            return hashCode;
        }

        public static bool operator ==(Game left, Game right)
        {
            return EqualityComparer<Game>.Default.Equals(left, right);
        }

        public static bool operator !=(Game left, Game right)
        {
            return !(left == right);
        }
        #endregion
    }
}
