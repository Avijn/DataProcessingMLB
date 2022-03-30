using DataProcessingMLB.VM;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
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

        public List<LinkTable> GetTeams(int year)
        {
            List<string> teamNames = new List<string>();
            using (StreamReader stream = new StreamReader(@"..\..\Datasets\linktable.json"))
            {
                string json = stream.ReadToEnd();
                List<LinkTable> tempList = JsonConvert.DeserializeObject<List<LinkTable>>(json);
                return tempList;
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
        
        public void CreateTeam(LinkTable linktable)
        {
            List<LinkTable> linkTables = new List<LinkTable>();
            using(StreamReader stream = new StreamReader(@"..\..\Datasets\linktable.json"))
            {
                string json = stream.ReadToEnd();
                linkTables = JsonConvert.DeserializeObject<List<LinkTable>>(json);
                linkTables.Add(linktable);
            }
            File.WriteAllText(@"..\..\Datasets\linktable.json", JsonConvert.SerializeObject(linkTables));
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

        public bool ValidateReturnGameAsJson(List<Game> list)
        {
            JSchema schema = JSchema.Parse(@"
                {
                  'title': 'Game',
                  'type': 'object',
                  'items': {
                    'title': 'Game',
                    'type': 'object',
                    'required': [
                      'team',
                      'home_away',
                      'opp',
                      'win_or_lose',
                      'rank',
                      'year',
                      'date',
                      'gameNumber'
                    ]
                  },
                  'properties': {
                    'game':{
                      'gameList': [
                        {
                          'team': {
                            '$id': '#root/items/team',
                            'title': 'team',
                            'type': 'string',
                            'default': '',
                            'pattern': '^.*$'
                          },
                          'home_away': {
                            '$id': '#root/items/home_away',
                            'title': 'home_away',
                            'type': 'string',
                            'default': '',
                            'pattern': '^.*$'
                          },
                          'opp': {
                            '$id': '#root/items/opp',
                            'title': 'opp',
                            'type': 'string',
                            'default': '',
                            'pattern': '^.*$'
                          },
                          'win_or_lose': {
                            '$id': '#root/items/win_or_lose',
                            'title': 'win_or_lose',
                            'type': 'string',
                            'default': '',
                            'pattern': '^.*$'
                          },
                          'rank': {
                            '$id': '#root/items/rank',
                            'title': 'rank',
                            'type': 'number',
                            'default': 0
                          },
                          'year': {
                            '$id': '#root/items/year',
                            'title': 'year',
                            'type': 'number',
                            'default': 0
                          },
                          'date': {
                            '$id': '#root/items/date',
                            'title': 'date',
                            'type': 'string',
                            'default': '',
                            'pattern': '^.*$'
                          },
                          'gameNumber': {
                            '$id': '#root/items/gameNumber',
                            'title': 'gameNumber',
                            'type': 'number',
                            'default': 0
                        }
                      ]
                    }
                  }
                }
            ");

            foreach (Game game in list)
            {
                string temp = JsonConvert.SerializeObject(game);
                JObject jObject = JObject.Parse(temp);
                bool valid = jObject.IsValid(schema);

                if (!valid)
                {
                    return false;
                }
            }
            return true;
        }

        public bool ValidateReturnRankingAsJson(List<Ranking> list)
        {
            JSchema schema = JSchema.Parse(@"
                {
                  'title': 'Rank',
                  'type': 'object',
                  'items': {
                    'title': 'Rank',
                    'type': 'object',
                    'required': [
                      'rank',
                      'team'
                    ]
                  },
                  'properties': {
                    'game':{
                      'gameList': [
                        {
                          'rank': {
                            '$id': '#root/items/rank',
                            'title': 'rank',
                            'type': 'number',
                            'default': 0
                          },
                          'team': {
                            '$id': '#root/items/team',
                            'title': 'team',
                            'type': 'string',
                            'default': '',
                            'pattern': '^.*$'
                          }
                        }
                      ]
                    }
                  }
                }
            ");

            foreach (Ranking rank in list)
            {
                string temp = JsonConvert.SerializeObject(rank);
                JObject jObject = JObject.Parse(temp);
                bool valid = jObject.IsValid(schema);

                if (!valid)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
