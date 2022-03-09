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
        [HttpGet]
        //public IEnumerable<BeerPriceObj> Get()
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Get()
        {
            try
            {
                return Ok(_mLBBeerManager.GetMLBBeerPriceAll());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return NotFound("Team is not found");
            }
        }

        // GET api/<MLBBeerController>/<id>
        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<BeerPriceObj>> Get(string name)
        {
            try
            {
                return Ok(_mLBBeerManager.GetMLBBeerPriceFromClub(name));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return NotFound("Team is not found");
            }
        }

        /// POST
        /// <summary>
        /// All POST requests
        /// </summary>

        // POST api/<MLBBeerController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public void Post([FromBody] string value)
        {
        }

        /// PUT
        /// <summary>
        /// All PUT requests
        /// </summary>

        // PUT api/<MLBBeerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        /// DELETE
        /// <summary>
        /// All DELETE requests
        /// </summary>

        // DELETE api/<MLBBeerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
