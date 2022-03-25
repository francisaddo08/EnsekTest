using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Domain.Models;
using Domain.entities;
using BackEndApiEF.data;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackEndApiEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private ILogger<AccountController> _logger;
        private ProjectDbContext _db;
        public AccountController(ProjectDbContext db, ILogger<AccountController> logger)
        {
            this._db = db;
            this._logger = logger;
        }

        // GET: api/<AccountController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AccountController>
        [HttpPost]
        public IActionResult Post([FromBody] List<AccountModel> accountModels)
        {
            if (ModelState.IsValid)
            {
                return Ok();
               

                // re-render the view when validation failed.
                //return View(model);
            }
            else {
                return BadRequest(ModelState);

            }
           
        }
        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AccountController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
