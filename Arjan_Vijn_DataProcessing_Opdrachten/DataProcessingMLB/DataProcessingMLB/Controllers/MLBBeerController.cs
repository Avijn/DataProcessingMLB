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
    public class MLBBeerController : ControllerBase
    {
        private readonly MLBBeerManager _mLBBeerManager;
        public MLBBeerController()
        {
            _mLBBeerManager = new MLBBeerManager();
        }

        /// GET
        /// <summary>
        /// All GET requests
        /// </summary>

        // GET: api/<MLBBeerController>
        // Returns all MLB beer data
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<BeerPriceObj>> Get()
        {
            Request.Headers.TryGetValue("Accept", out Microsoft.Extensions.Primitives.StringValues value);

            try
            {
               return Ok(_mLBBeerManager.GetMLBBeerPriceAll(value.ToString()));
            }
            catch (Exception)
            {
                return NotFound("Something went wrong!");
            }
        }

        // GET api/<MLBBeerController>/<name>
        // Returns MLB beer data from specific MLB club
        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<BeerPriceObj>> Get(string name)
        {
            Request.Headers.TryGetValue("Accept", out Microsoft.Extensions.Primitives.StringValues value);

            if(name.Contains("%20"))
            {
                name.Replace("%20", " ");
            }

            try
            {
                return Ok(_mLBBeerManager.GetMLBBeerPriceFromClub(name, value));
            }
            catch (Exception)
            {
                return NotFound("Team is not found");
            }
        }

        /// POST
        /// <summary>
        /// All POST requests
        /// </summary>

        // POST api/<MLBBeerController>/<BeerPriceObj>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<string> Post([FromBody] BeerPriceObj beerpriceobj)
        {
            try
            {
                _mLBBeerManager.CreateBeerPrice(beerpriceobj);
            }
            catch (Exception)
            {
                return NotFound("Error, beer price could not be created!");
            }
            return Ok("Beer price is created");
        }

        /// PUT
        /// <summary>
        /// All PUT requests
        /// </summary>

        // PUT api/<MLBBeerController>/<BeerPriceObj>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<string> Put([FromBody] BeerPriceObj beerpriceobj)
        {
            try
            {
                if (_mLBBeerManager.ChangeBeerPrice(beerpriceobj))
                {
                    return Ok("Beer price edited");
                }
                else
                {
                    return NotFound("Could not find team/year");
                }
            }
            catch (Exception)
            {
                return NotFound("Could not find team/year");
            }
        }

        /// DELETE
        /// <summary>
        /// All DELETE requests
        /// </summary>

        // DELETE api/<MLBBeerController>/<team , year>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<string> DeleteBeerPrice(string team, int year)
        {
            try
            {
                if (_mLBBeerManager.DeleteBeerPrice(team, year))
                {
                    return Ok("Beer price is deleted!");
                }
                else
                {
                    return NotFound("Error, beer price is not deleted");
                }
            }
            catch (Exception)
            {
                return NotFound("Error, beer price is not deleted");
            }
        }
    }
}
