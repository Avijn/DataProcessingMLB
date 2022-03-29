using DataProcessingMLB.BL;
using DataProcessingMLB.VM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace DataProcessingMLB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MLBGamesController : ControllerBase
    {
        private readonly TeamRankingManager _teamRankingManager;
        public MLBGamesController()
        {
            _teamRankingManager = new TeamRankingManager();
        }

        /// GET
        /// <summary>
        /// All GET requests
        /// </summary>
        
        // GET api/<MLBGamesController>/<name, year>
        [HttpGet("{name} {year}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<Game>> GetGames(string name, int year)
        {
            Request.Headers.TryGetValue("Accept", out Microsoft.Extensions.Primitives.StringValues value);
            try
            {
                return Ok(_teamRankingManager.GetGames(name, year, value.ToString()));
            }
            catch (Exception)
            {
                return NotFound("No matches found");
            }
        }

        // GET: api/<MLBGamesController>/<year>
        [HttpGet("{year}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<Ranking>> GetRanking(int year)
        {
            Request.Headers.TryGetValue("Accept", out Microsoft.Extensions.Primitives.StringValues value);
            try
            {
                return Ok(_teamRankingManager.GetRanking(year, value));
            }
            catch(Exception)
            {
                return NotFound("No ranking found for given year");
            }
        }

        // GET: api/<MLBGamesController>/Getteams/<year>
        [HttpGet("GetAllTeams{year}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<LinkTable>> GetTeams(int year)
        {
            try
            {
                return Ok(_teamRankingManager.GetTeams(year));
            }
            catch (Exception)
            {
                return NotFound("No teams found for given year");
            }
        }

        /// POST
        /// <summary>
        /// All POST requests
        /// </summary>

        // POST api/<MLBGamesController>/<Game>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<string> CreateMatchResult([FromBody] Game game)
        {
            try
            {
                _teamRankingManager.CreateMatchResult(game);
            }
            catch (Exception)
            {
                return NotFound("Error, match result could not be created!");
            }
            return Ok("Match result is created!");
        }

        /// PUT
        /// <summary>
        /// All PUT requests
        /// </summary>

        // PUT api/<MLBGamesController>/<Game>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<string> EditMatchResult([FromBody] Game game)
        {
            try
            {
                if(_teamRankingManager.EditMatchResult(game))
                {
                    return Ok("Match result is edited!");
                }
                else
                {
                    return NotFound("Error, match result is not edited!");
                }
            }
            catch (Exception)
            {
                return NotFound("Error, match result is not edited!");
            }
        }

        /// DELETE
        /// <summary>
        /// All DELETE requests
        /// </summary>
        /// 
        // PUT api/<MLBGamesController>/<team, year, g>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<string> Delete(string team, int year, int g)
        {
            try
            {
                if (_teamRankingManager.DeleteGame(team, year, g))
                {
                    return Ok("Match result is deleted!");
                }
                else
                {
                    return NotFound("Error, match result is not deleted!");
                }
            }
            catch (Exception)
            {
                return NotFound("Error, match result is not deleted!");
            }
        }
    }
}
