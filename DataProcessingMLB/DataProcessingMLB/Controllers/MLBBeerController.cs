using DataProcessingMLB.BL;
using DataProcessingMLB.VM;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
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
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<BeerPriceObj>> Get()
        {
            try
            {
                List<BeerPriceObj> tempBeerPriceList = _mLBBeerManager.GetMLBBeerPriceAll(name, year);
                JSchema schema = JSchema.Parse(@"./json/BeercostSchema.json");
                List<string> GameList = new List<string>();
                foreach (BeerPriceObj beerPrice in tempBeerPriceList)
                {
                    string temp = JsonConvert.SerializeObject(beerPrice);
                    JObject jObject = JObject.Parse(temp);
                    bool valid = jObject.IsValid(schema);

                    if (valid)
                    {
                        GameList.Add(temp);
                    }

                }
                return Ok(_mLBBeerManager.GetMLBBeerPriceAll());
            }
            catch (Exception)
            {
                return NotFound("Something went wrong!");
            }
        }

        // GET api/<MLBBeerController>/<name>
        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<BeerPriceObj>> Get(string name)
        {
            try
            {
                return Ok(_mLBBeerManager.GetMLBBeerPriceFromClub(name));
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
            catch(Exception)
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
                if(_mLBBeerManager.DeleteBeerPrice(team, year))
                {
                    return Ok("Beer price is deleted!");
                }
                else
                {
                    return NotFound("Error, beer price is not deleted");
                }
            }
            catch(Exception)
            {
                return NotFound("Error, beer price is not deleted");
            }
        }
    }
}
