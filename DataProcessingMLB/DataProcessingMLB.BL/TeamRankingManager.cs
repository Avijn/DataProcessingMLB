using DataProcessingMLB.DAL;
using DataProcessingMLB.VM;
using System;
using System.Collections.Generic;

namespace DataProcessingMLB.BL
{
    public class TeamRankingManager
    {
        private readonly TeamRankingService _teamRankingService;
        public TeamRankingManager()
        {
            _teamRankingService = new TeamRankingService();
        }

        public List<Game> GetGames(string name, int year)
        {
            return _teamRankingService.GetGames(name, year);
        }

        public List<Ranking> GetRanking(int year)
        {
            return _teamRankingService.GetRanking(year);
        }

        public List<string> GetTeams(int year)
        {
            return _teamRankingService.GetTeams(year);
        }

        public void CreateMatchResult(Game game)
        {
            _teamRankingService.CreateMatchResult(game);
        }

        public bool EditMatchResult(Game game)
        {
            return _teamRankingService.EditMatchResult(game);
        }

        public bool DeleteGame(string name, int year, int g)
        {
            return _teamRankingService.DeleteGame(name, year, g);
        }
    }
}
