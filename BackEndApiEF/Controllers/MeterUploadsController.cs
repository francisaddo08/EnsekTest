using LumenWorks.Framework.IO.Csv;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net.Http.Headers;
using BackEndApiEF.data;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace BackEndApiEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeterUploadsController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private ProjectDbContext _db;
        private ILogger<MeterUploadsController> _loger;
        public MeterUploadsController(ProjectDbContext db, ILogger<MeterUploadsController> log, IWebHostEnvironment web)
        {
            this._db = db;
            this._loger = log;
            this._hostEnvironment = web;
        }
        [HttpPost("OnPostUploadAsync"), DisableRequestSizeLimit]
        public async Task<IActionResult> OnPostUploadAsync(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var filePath = Path.GetTempFileName();

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { count = files.Count, size });
        }



        [HttpPost("meter-reading-uploads"), DisableRequestSizeLimit]
        public IActionResult meterUploads()
        {
            try {

                var uploadedFile = Request.Form.Files[0];
                var folderName = Path.Combine("uploads", "files");
                var saveTo = Path.Combine(Directory.GetCurrentDirectory(), folderName);

               
                if (uploadedFile.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(uploadedFile.ContentDisposition).FileName.Trim();
                    var fullPath = Path.Combine(saveTo, fileName);
                 
                    return Ok(fileName);
                }
                else
                {
                    return BadRequest();
                }
            } 
            catch
            {
                return BadRequest();
            }
            

          
        }
        private bool CheckIfExcelFile(IFormFile file)
        {
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            return (extension == ".csv"); 
        }
    }
}
