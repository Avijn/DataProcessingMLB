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

    }
}
