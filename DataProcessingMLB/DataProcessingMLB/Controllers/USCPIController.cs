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
    public class USCPIController : ControllerBase
    {
        private readonly USCPIManager _uSCPIManager;

        public USCPIController()
        {
            _uSCPIManager = new USCPIManager();
        }

        /// GET
        /// <summary>
        /// All GET requests
        /// </summary>
        
        // GET: api/<USCPIController>
        [HttpGet("{year}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<USCPIModel>> Getyear(int year)
        {
            return _uSCPIManager.GetYear(year);
        }

        // GET api/<USCPIController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<USCPIController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<USCPIController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<USCPIController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
