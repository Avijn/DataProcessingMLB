using DataProcessingMLB.VM;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace DataProcessingMLB.DAL
{
    public class TeamRankingService
    {
        private readonly string path;

        public TeamRankingService()
        {
            path = @"..\..\Datasets\MLBGames\MLBGames.json";
        }

        public List<Game> GetGames(string name, int year)
        {
            // The seasons dont end at the end of the year
            // Some seasons start at the end of a year
            //TODO fix this!

            using (StreamReader stream = new StreamReader(path))
            {
                string json = stream.ReadToEnd();
                List<Game> allGames = JsonConvert.DeserializeObject<List<Game>>(json);
                List<Game> games = new List<Game>();
                foreach (Game game in allGames)
                {
                    if (game.team == name)
                    {
                        if (game.year == year)
                        {
                            games.Add(game);
                        }
                    }
                }

                if (games.Count > 0)
                {
                    return games;
                }
            }
            throw new Exception("This team doesn't exsist");
        }

        public List<Ranking> GetRanking(int year)
        {
            using (StreamReader stream = new StreamReader(path))
            {
                string json = stream.ReadToEnd();
                List<Game> allGames = JsonConvert.DeserializeObject<List<Game>>(json);
                List<Ranking> ranking = new List<Ranking>();
                bool exsists = false;

                foreach (Game game in allGames)
                {
                    if (game.year == year)
                    {
                        if(ranking.Count != 0)
                        {
                            for (int i = 0; i < ranking.Count; i++)
                            {
                                if (game.team == ranking[i].team)
                                {
                                    exsists = true;
                                    ranking[i].rank = game.rank;
                                }
                            }                            
                        }

                        if (exsists == false)
                        {
                            Ranking currentRank = new Ranking();
                            currentRank.team = game.team;
                            currentRank.rank = game.rank;

                            ranking.Add(currentRank);
                        }

                        exsists = false;
                    }
                }

                return ranking;
            }
        }
    }
}
