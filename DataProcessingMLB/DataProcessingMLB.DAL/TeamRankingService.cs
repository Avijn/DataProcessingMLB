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
            //TODO The seasons dont end at the end of the year, Some seasons start at the end of a year

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
                        if (ranking.Count != 0)
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

        public void CreateMatchResult(Game game)
        {
            List<Game> GamesList = new List<Game>();
            using (StreamReader stream = new StreamReader(path))
            {
                string json = stream.ReadToEnd();
                GamesList = JsonConvert.DeserializeObject<List<Game>>(json);
                GamesList.Add(game);
            }
            File.WriteAllText(path, JsonConvert.SerializeObject(GamesList));
        }

        public bool EditMatchResult(Game game)
        {
            bool objFound = false;
            List<Game> GamesList = new List<Game>();
            using (StreamReader stream = new StreamReader(path))
            {
                string json = stream.ReadToEnd();
                GamesList = JsonConvert.DeserializeObject<List<Game>>(json);

                foreach (Game obj in GamesList)
                {
                    if (obj.team == game.team && obj.year == game.year && obj.g == game.g)
                    {
                        objFound = true;
                        obj.win_or_lose = game.win_or_lose;
                    }
                }
            }
            File.WriteAllText(path, JsonConvert.SerializeObject(GamesList));
            return objFound;
        }

        public bool DeleteGame(string team, int year, int g)
        {
            bool objFound = false;
            List<Game> GamesList = new List<Game>();
            using (StreamReader stream = new StreamReader(path))
            {
                string json = stream.ReadToEnd();
                GamesList = JsonConvert.DeserializeObject<List<Game>>(json);

                foreach (Game obj in GamesList)
                {
                    if(obj.team == team && obj.year == year && obj.g == g)
                    {
                        GamesList.Remove(obj);
                        objFound = true;
                        break;
                    }
                }
            }
            File.WriteAllText(path, JsonConvert.SerializeObject(GamesList));
            return objFound;
        }
    }
}
