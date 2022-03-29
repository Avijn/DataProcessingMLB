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
            Request.Headers.TryGetValue("Accept", out Microsoft.Extensions.Primitives.StringValues value);
            return _uSCPIManager.GetYear(year, value);
        }

        // POST api/<USCPIController>
        [HttpPost]
        public ActionResult<string> Post([FromBody] USCPIModel value)
        {
            try
            {
                _uSCPIManager.Post(value);
            }
            catch (Exception)
            {
                return NotFound("Error, USCPI entry could not be created!");
            }
            return Ok("USCPI entry is created!");
        }

        // PUT api/<USCPIController>/
        [HttpPut("{date}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<string> Put(USCPIModel model)
        {
            try
            {
                if (_uSCPIManager.Put(model))
                {
                    return Ok("USCPI succesfully changed!");
                }
                else
                {
                    return NotFound("Given date isn't found as a entry!");
                }
            }
            catch (Exception)
            {
                return NotFound("Something went wrong!");
            }
        }

        // DELETE api/<USCPIController>/5
        [HttpDelete("{date}")]
        public ActionResult<string> Delete(string date)
        {
            try
            {
                if (_uSCPIManager.Delete(date))
                {
                    return Ok("USCPI succesfully deleted!");
                }
                else
                {
                    return NotFound("Given date isn't found as a entry!");
                }
            }
            catch (Exception)
            {
                return NotFound("Something went wrong!");
            }
        }
    }
}
