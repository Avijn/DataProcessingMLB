using DataProcessingMLB.BL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace DataProcessingMLB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MLBBeerController : ControllerBase
    {
        private readonly MLBBeerPrice _mLBBeerPrices;  
        public MLBBeerController()
        {
            _mLBBeerPrices = new MLBBeerPrice();
        }

        /// GET
        /// <summary>
        /// All GET requests
        /// </summary>

        // GET: api/<MLBBeerController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<MLBBeerController>/<id>
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        /// POST
        /// <summary>
        /// All POST requests
        /// </summary>

        // POST api/<MLBBeerController>
        [HttpPost]
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
