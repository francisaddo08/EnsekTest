using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Domain.entities;
using System.Threading.Tasks;
using System.Data;
using BackEndApiEF.validators;
using Domain.Models;
using Microsoft.Extensions.Logging;
using BackEndApiEF.data;
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.IO;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackEndApiEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeterReadingController : ControllerBase
    {
        private ILogger<MeterReadingController> _logger;
        private ProjectDbContext _db;
        public MeterReadingController(ProjectDbContext db, ILogger<MeterReadingController> logger)
        {
            this._db = db;
            this._logger = logger;
        }
        // GET: api/<MeterReadingController>
        [HttpGet]
        public DataTable Get()
        {
            List<Account> listAc = new List<Account>();
            if (CVSFileReaderLibray.CsvDataReader.GetMeterDataFromFile() != null)
            {
               
                return CVSFileReaderLibray.CsvDataReader.GetMeterDataFromFile();
            }
            return CVSFileReaderLibray.CsvDataReader.GetMeterDataFromFile();
        }
        [HttpPost("meterreadinguploads", Name = "meterreadinguploads")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult meterReadingUploads(IFormFile file)
        {
            if (CheckIfExcelFile(file))
            {

            }
            else
            {
                return BadRequest(new { message = "Invalid file extension" });
            }

            return Ok();
        }


        // GET api/<MeterReadingController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MeterReadingController>
        [HttpPost("Post")]
        public IActionResult Post([FromBody] List<Domain.Models.MeterReadModel> modellist)
     {
            
            MeterReadValidator validator = new MeterReadValidator();
            List<MeterReadModel> failedModels = new List<MeterReadModel>(); //for failed models
            List<MeterRead> successfulModels = new List<MeterRead>();
            MeterReadingResponseMode response = new MeterReadingResponseMode();
            foreach (MeterReadModel m in modellist)
       { 
                var validResult = validator.Validate(m);
                MeterRead meter = new MeterRead();
                if (validResult.IsValid)
          {
                    var accountId = _db.Accounts.Find(m.AccountId);
                    if(accountId != null)
                    {     
                        this._db.MeterReads.Add(new MeterRead
                            {  
                            AccountId = m.AccountId,
                            MeterReadingDateTime = m.MeterReadingDateTime,
                            MeterReadValue = m.MeterReadValue

                        });
                    }else
                    {
                        failedModels.Add(new MeterReadModel
                        {
                            AccountId = m.AccountId,
                            MeterReadingDateTime = m.MeterReadingDateTime,
                            MeterReadValue = m.MeterReadValue
                        });
                    } // no coresponding Account

                }
                else {
                    return BadRequest(validResult.Errors);

                } // mpodel not valid

            }
            if (this._db.SaveChanges() > 0)
            {
                List<MeterRead> readMeterData = this._db.MeterReads.ToList();
                response.sucessfulMeterReads = readMeterData;
                response.failedMeterReads = failedModels;
                 return Created($"", response);
            }
            else
            {
                return BadRequest(this.StatusCode(500, "Your Meter Reading could not be saved"));
            }
            
        }

        [Route("api/MeterReading/meter-reading-uploads/")]
        public IActionResult meterReadingUploads([FromBody] string value)
        {
            return Ok(value);
        }

        // PUT api/<MeterReadingController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MeterReadingController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        private bool CheckIfExcelFile(IFormFile file)
        {
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            return (extension == ".xlsx" || extension == ".xls"); // Change the extension based on your need
        }
        private async Task<bool> WriteFile(IFormFile file)
        {
            bool isSaveSuccess = false;
            string fileName;
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                fileName = DateTime.Now.Ticks + extension; //Create a new Name for the file due to security reasons.

                var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\files");

                if (!Directory.Exists(pathBuilt))
                {
                    Directory.CreateDirectory(pathBuilt);
                }

                var path = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\files",
                   fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                isSaveSuccess = true;
            }
            catch (Exception e)
            {
                
            }

            return isSaveSuccess;
        }
    }
}
