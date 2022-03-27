using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Domain.entities;
using Domain.Models;
using BackEndApiEF.repo;
using BackEndApiEF.Controllers;
using System.Net.Http;
using System.IO;
using Microsoft.AspNetCore.Http;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using FakeItEasy;

namespace Billing.Test
{
    public class MeterControllerTest
    {
        private readonly MeterController _controller;
        private readonly BillingRepoFake _service;

        public MeterControllerTest()
        {
            _service = new BillingRepoFake();
            _controller = new MeterController(_service);
        }
        [Fact]
        public async Task InvalidFileExtension_ReturnBadRequest_StatusCode400()
        {
            // Arrange
           BadRequestObjectResult response;

          
            var file = "C:/Ensek/api/BillingEnsekTest/Domain/data/Wrong_File_Extension.txt";
            using var stream = new MemoryStream(File.ReadAllBytes(file).ToArray());
            var formFile = new FormFile(stream, 0, stream.Length, "streamFile", file.Split(@"\").Last());

         
            // Act
            response = (BadRequestObjectResult)await _controller.UploadFile(formFile);
            // Assert
           int a = response.StatusCode.GetValueOrDefault();
            Assert.Equal(400, a);
            
                
        }
        [Fact]
        public async Task NoValidData_ReturnBadRequest_StatusCode400()
        {
            // Arrange
            BadRequestObjectResult response;

        
        var file = "C:/Ensek/api/BillingEnsekTest/Domain/data/Meter_Reading_No_Valid_Data.csv";
            using var stream = new MemoryStream(File.ReadAllBytes(file).ToArray());
            var formFile = new FormFile(stream, 0, stream.Length, "streamFile", file.Split(@"\").Last());


            // Act
            response = (BadRequestObjectResult)await _controller.UploadFile(formFile);
            // Assert
            int a = response.StatusCode.GetValueOrDefault();
            Assert.Equal(400, a);


        }
        [Fact]
        public async Task ValidData_Saved_Returned_Created()
        {
            // Arrange
           

        
        var file = "C:/Ensek/api/BillingEnsekTest/Domain/data/Test_Accounts.csv";
            using var stream = new MemoryStream(File.ReadAllBytes(file).ToArray());
            var formFile = new FormFile(stream, 0, stream.Length, "streamFile", file.Split(@"\").Last());


            // Act
            var createdResult =    _controller.UploadFile(formFile);

      

            //Assert
            Assert.IsType<CreatedResult>(createdResult.IsCompletedSuccessfully);
            var item = createdResult.Result as CreatedResult;



            var meterReadResponseModel = new MeterReadingResponseMode();
            var respons = item.Value as MeterReadingResponseMode;
            Assert.IsType<MeterReadingResponseMode>(respons);
           



        }
    }
}
