using BackEndApiEF.data;
using BackEndApiEF.validators;
using Domain.entities;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndApiEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadsController : ControllerBase
    {
        string filePath = "";
        private ILogger<UploadsController> _logger;
        private ProjectDbContext _db;

        public UploadsController(ProjectDbContext db, ILogger<UploadsController> logger)
        {
            this._db = db;
            this._logger = logger;
        }
        [HttpPost("meter-reading-uploads", Name = "meter-reading-uploads")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadFile(
         IFormFile file)
        {

            if (CheckIfCsvFile(file))
            {
                await WriteFile(file);
                List<MeterReadModel> fileData = CVSFileReaderLibray.CsvDataReader.GetMeterData(this.filePath);
                if ( fileData != null)
                {
                    MeterReadValidator validator = new MeterReadValidator();
                    List<MeterReadModel> failedModels = new List<MeterReadModel>(); //for failed models
                    List<MeterRead> successfulModels = new List<MeterRead>();
                    MeterReadingResponseMode response = new MeterReadingResponseMode();
                    foreach (MeterReadModel m in fileData)
                    {
                        var validResult = validator.Validate(m);
                        MeterRead meter = new MeterRead();
                        if (validResult.IsValid)
                        {
                            var accountId = await _db.Accounts.FindAsync(m.AccountId);
                            if (accountId != null)
                            {
                                this._db.MeterReads.Add(new MeterRead
                                {
                                    AccountId = m.AccountId,
                                    MeterReadingDateTime = m.MeterReadingDateTime,
                                    MeterReadValue = m.MeterReadValue

                                });
                            }
                            else
                            {
                                failedModels.Add(new MeterReadModel
                                {
                                    AccountId = m.AccountId,
                                    MeterReadingDateTime = m.MeterReadingDateTime,
                                    MeterReadValue = m.MeterReadValue
                                });
                            } // no coresponding Account

                        }
                        else
                        {
                            return BadRequest(validResult.Errors);

                        } // model not valid

                    }
                    if (this._db.SaveChanges() > 0)
                    {
                        List<MeterRead> readMeterData = await this._db.MeterReads.ToListAsync();
                        response.sucessfulMeterReads = readMeterData;
                        response.failedMeterReads = failedModels;
                        return Created($"", response);
                    }
                    else
                    {
                        return BadRequest(this.StatusCode(500, "Your Meter Reading could not be saved"));
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest(new { message = "Invalid file extension" });
            }

          

        }
        private bool CheckIfCsvFile(IFormFile file)
        {
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            return (extension == ".csv");
        }
        private async Task<bool> WriteFile(IFormFile file)
        {
            bool isSaveSuccess = false;
            string fileName;
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                fileName = DateTime.Now.Ticks + extension; 

                var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "upload\\files");

                if (!Directory.Exists(pathBuilt))
                {
                    Directory.CreateDirectory(pathBuilt);
                }

                var path = Path.Combine(Directory.GetCurrentDirectory(), "upload\\files",
                   fileName);
                this.filePath = path;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                isSaveSuccess = true;
            }
            catch (Exception e)
            {
                //log error
            }

            return isSaveSuccess;
        }
    }
}
