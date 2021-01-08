using System;
using System.Linq;
using TestTask_TDD_ScoreboardFootball;
using Xunit;

namespace XUnitTestProject
{
    public class ScoreboardTDD
    {
        [Fact]
        public void TestStartGame()
        {
            var scoreboard = new Scoreboard();

            Team mexicoTeam = new Team("Mexico");
            Team canadaTeam = new Team("Canada");

            scoreboard.StartGame(mexicoTeam, canadaTeam);

            Assert.Single(scoreboard.GetGames());
        }
        
        [Fact]
        public void TestStartGameNullTeam()
        {
            var scoreboard = new Scoreboard();

            Team canadaTeam = new Team("Canada");
            Assert.Throws<ArgumentNullException>(() => scoreboard.StartGame(null, canadaTeam));
            Assert.Throws<ArgumentNullException>(() => scoreboard.StartGame(canadaTeam, null));
            Assert.Throws<ArgumentNullException>(() => scoreboard.StartGame(null, null));
        }

        [Fact]
        public void TestStartGameBothTeamsAreSame()
        {
            var scoreboard = new Scoreboard();

            Team canadaTeam = new Team("Canada");
            Assert.Throws<ArgumentException>(() => scoreboard.StartGame(canadaTeam, canadaTeam));
        }

        [Fact]
        public void TestStartGameWithSameTeams()
        {
            var scoreboard = new Scoreboard();

            Team mexicoTeam = new Team("Mexico");
            Team canadaTeam = new Team("Canada");

            scoreboard.StartGame(mexicoTeam, canadaTeam);

            Assert.Throws<ArgumentException>(() => scoreboard.StartGame(mexicoTeam, canadaTeam));

            Team brazilTeam = new Team("Brazil");
            Team mexicoTeamLowerCase = new Team("mexico");
            Assert.Throws<ArgumentException>(() => scoreboard.StartGame(mexicoTeamLowerCase, brazilTeam));

            Team spainTeam = new Team("Spain");

            Assert.Throws<ArgumentException>(() => scoreboard.StartGame(mexicoTeam, spainTeam));
        }

        [Fact]
        public void TestUpdateScore()
        {
            var scoreboard = new Scoreboard();

            Team mexicoTeam = new Team("Mexico");
            Team canadaTeam = new Team("Canada");

            var game = scoreboard.StartGame(mexicoTeam, canadaTeam);

            Assert.Equal(0, game.HomeTeamPoints);

            game.UpdateScore(1, 0);

            Assert.Equal(1, game.HomeTeamPoints);
        }

        [Fact]
        public void TestStartAndFinishGame()
        {
            var scoreboard = new Scoreboard();

            Team mexicoTeam = new Team("Mexico");
            Team canadaTeam = new Team("Canada");

            var game = scoreboard.StartGame(mexicoTeam, canadaTeam);

            Assert.Single(scoreboard.GetGames());

            game.FinishGame();

            Assert.Empty(scoreboard.GetGames());

            Assert.Throws<ArgumentException>(() => game.FinishGame());
        }

        [Fact]
        public void TestGetSummary()
        {
            var scoreboard = new Scoreboard();


            Team mexicoTeam = new Team("Mexico");
            Team canadaTeam = new Team("Canada");

            var gameMexicoCanada = scoreboard.StartGame(mexicoTeam, canadaTeam);

            gameMexicoCanada.UpdateScore(2, 1);

            Assert.Equal("Mexico - Canada: 2 - 1", scoreboard.GetSummary().Trim());


            Team spainTeam = new Team("Spain");
            Team brazilTeam = new Team("Brazil");

            var gameSpainBrazil = scoreboard.StartGame(spainTeam, brazilTeam);

            gameSpainBrazil.UpdateScore(4, 4);

            Assert.Equal(
                "Spain - Brazil: 4 - 4" + Environment.NewLine +
                "Mexico - Canada: 2 - 1",
                scoreboard.GetSummary().Trim());


            Team germanyTeam = new Team("Germany");
            Team franceTeam = new Team("France");

            var gameGermanyFrance = scoreboard.StartGame(germanyTeam, franceTeam);

            gameGermanyFrance.UpdateScore(5, 3);

            Assert.Equal(
                "Spain - Brazil: 4 - 4" + Environment.NewLine +
                "Germany - France: 5 - 3" + Environment.NewLine +
                "Mexico - Canada: 2 - 1",
                scoreboard.GetSummary().Trim());


            gameGermanyFrance.UpdateScore(5, 4);

            Assert.Equal(
              "Germany - France: 5 - 4" + Environment.NewLine +
              "Spain - Brazil: 4 - 4" + Environment.NewLine +
              "Mexico - Canada: 2 - 1",
              scoreboard.GetSummary().Trim());


            gameSpainBrazil.FinishGame();

            Assert.Equal(
             "Germany - France: 5 - 4" + Environment.NewLine +
             "Mexico - Canada: 2 - 1",
             scoreboard.GetSummary().Trim());
        }
    }
}
