using BackEndApiEF.repo;
using BackEndApiEF.validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using Domain.entities;
using Domain.Models;
using BackEndApiEF.Helper;
using System.Collections.Generic;

namespace BackEndApiEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeterController : ControllerBase
    {
        public IBillingRepo _repository { get; }
        public MeterController(IBillingRepo repository)
        {
            _repository = repository;
        }
        [HttpPost("meter-reading-uploads", Name = "meter-reading-uploads")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MeterReadingResponseMode), 200)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            DateTime createdDate = DateTime.Now;
            try {
               
                if (FileUpLoadHelper.CheckIfCsvFile(file))
                {
                    if (await FileUpLoadHelper.WriteFile(file))
                    {
                        List<MeterReadModel> fileData = CVSFileReaderLibray.CsvDataReader.GetMeterData(FileUpLoadHelper.filePath);
                        if (fileData != null)
                        {
                            MeterReadValidator validator = new MeterReadValidator();
                            List<MeterReadModel> failedModels = new List<MeterReadModel>(); //for failed models
                            List<MeterRead> SaveModels = new List<MeterRead>(); //for saved models

                            MeterReadingResponseMode response = new MeterReadingResponseMode();
                            foreach (MeterReadModel m in fileData)
                            {
                                var validResult = validator.Validate(m);
                                MeterRead meter = new MeterRead();
                                if (validResult.IsValid)
                                {
                                    //var accountId = await _db.Accounts.FindAsync(m.AccountId);
                                    var accountId = this._repository.FindAccount(m.AccountId);
                                    if (accountId != null)
                                    {
                                        SaveModels.Add(new MeterRead
                                        {
                                            AccountId = m.AccountId,
                                            MeterReadingDateTime = m.MeterReadingDateTime,
                                            CreatedOnDate = createdDate,
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
                                    FileUpLoadHelper.deleteTempFile();
                                    return BadRequest(validResult.Errors);

                                } // model not valid

                            }
                            if(SaveModels.Count < 1)
                            {
                                return BadRequest(this.StatusCode(400, "Your Meter Reading have no Valid data"));
                            }
                            this._repository.AddRange(SaveModels);
                            if (this._repository.Save() > 0)
                            {
                                List<MeterRead> readMeterData = await this._repository.currentMeterReading(createdDate);
                                response.sucessfulMeterReads = readMeterData;
                                response.failedMeterReads = failedModels;
                                FileUpLoadHelper.deleteTempFile();
                                return Created($"api/Meter/meter-reading-uploads", response);
                            }
                            else
                            {
                                return BadRequest(this.StatusCode(500, "Your Meter Reading could not be saved"));
                            }
                        }
                        else
                        {
                            return BadRequest(this.StatusCode(400, "Your Meter Reading have no data"));
                        }
                    }
                    else
                    {
                        return BadRequest(this.StatusCode(500, "Your Meter Reading could not be saved"));
                    }
                }
                else
                {
                    return BadRequest(this.StatusCode(400, "Invalid file extension"));
                }




            }
            catch(Exception ex)
            {
                return StatusCode( 500, $"Internal Server Error{ex}");
            }
         

        }
     
    }
}
