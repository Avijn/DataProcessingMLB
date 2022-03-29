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

        public List<Game> GetGames(string name, int year, string value)
        {
            List<Game> list = _teamRankingService.GetGames(name, year);
            if (value != "application/xml")
            {
                //if (_teamRankingService.ValidateReturnGameAsJson(list))
                //{
                    return list;
                //}
            }
            if (value == "application/xml")
            {
                return _teamRankingService.GetGames(name, year);
            }

            throw new Exception("Something went wrong!");
        }

        public List<Ranking> GetRanking(int year, string value)
        {
            List<Ranking> list = _teamRankingService.GetRanking(year);
            if(value != "application/xml")
            {
                //if(_teamRankingService.ValidateReturnRankingAsJson(list))
                //{
                    return list;
                //}
            }
            if (value == "application/xml")
            {
                return list;
            }
            throw new Exception("Something went wrong!");
        }

        public List<LinkTable> GetTeams(int year)
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
