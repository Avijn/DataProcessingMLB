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

        // GET: MLBGames
        [HttpGet("{name} {year}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<Game>> Get(string name, int year)
        {
            try
            {
                return Ok(_teamRankingManager.GetGames(name, year));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return NotFound("Team is not found");
            }
        }

        // GET: MLBGames/Ranking
        [HttpGet("{year}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<Ranking>> GetRanking(int year)
        {
            return Ok(_teamRankingManager.GetRanking(year));
        }

        // GET: MLBGames/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: MLBGames/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: MLBGames/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: MLBGames/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: MLBGames/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: MLBGames/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: MLBGames/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
