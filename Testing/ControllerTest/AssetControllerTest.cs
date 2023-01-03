using AddressApi.Contracts;
using AddressApi.Controllers;
using AddressApi.Entities.DTOs.RequestDto;
using AddressApi.Repository;
using AddressApi.Service;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.ControllerTest
{
    public class AssetControllerTest
    {
        private readonly IJWTManagerRepository jWTManagerRepository;
        private readonly ILogger<AssetController> _logger;
        private readonly ILog _log;
        public AssetControllerTest() { }
        /// <summary>
        /// Method to Test Upload File API
        /// </summary>
        /// <returns></returns>
        [Fact]
        public void UplaodFile_ValidId_ReturnFileDetails()
        {
            var db = TestData.inmemory();

            var repository = new AccountRepository(db);

            var accountservice = new AccountService(repository);

            var asset = new AssetController(accountservice, jWTManagerRepository,_logger);

            Guid Id = Guid.Parse("c572c99e-ee1f-4d17-b69c-08dae952ed26");
            var content = "Hello World from a Fake File";
            var fileName = "test.pdf";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;

            //create FormFile with desired data
            IFormFile file = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);
            FileDto newfile = new FileDto();
              newfile.file= file;
            var result = asset.UploadFile(Id, newfile);

            var finalresult = Assert.IsType<OkObjectResult>(result);

            UploadFileDto filedata = finalresult.Value as UploadFileDto;

            Assert.Equal("https://localhost:44350/api/asset?assetId=f875eb1a-f830-49b3-5df6-08dae9686397", filedata.DownloadUrl);
        }
        /// <summary>
        /// Method to Test Download File API
        /// </summary>
        /// <returns></returns>
        [Fact]
        public void DownloadFile_ReturnOkStatus()
        {
            var db = TestData.inmemory();

            var repository = new AccountRepository(db);

            var accountservice = new AccountService(repository);

            var asset = new AssetController(accountservice, jWTManagerRepository,_logger);

            Guid Id = Guid.Parse("f875eb1a-f830-49b3-5df6-08dae9686397");
            var result = asset.DownloadFile(Id);

            var finalresult = Assert.IsType<OkObjectResult>(result);
            FileContentResult filedata = finalresult.Value as FileContentResult;

            Assert.Equal("application/pdf", filedata.ContentType);
        }
        /// <summary>
        /// Method to Test Download File API
        /// </summary>
        /// <returns></returns>
        [Fact]
        public void DownloadFile_ReturnNotFound()
        {
            var db = TestData.inmemory();

            var repository = new AccountRepository(db);

            var accountservice = new AccountService(repository);

            var asset = new AssetController(accountservice, jWTManagerRepository, _logger);

            Guid Id = Guid.Parse("f875eb1a-f930-49b3-5df6-08dae9686397");
            var result = asset.DownloadFile(Id);
            var finalresult = Assert.IsType<NotFoundObjectResult>(result);

            Assert.Equal("File Not Found", finalresult.Value);
        }
        /// <summary>
        /// Method to Test Download File API
        /// </summary>
        /// <returns></returns>
        [Fact]
        public void DownloadFile_ReturnUnathorize()
        {
            var db = TestData.inmemory();

            var repository = new AccountRepository(db);

            var accountservice = new AccountService(repository);

            var asset = new AssetController(accountservice, jWTManagerRepository, _logger);

            Guid Id = Guid.Parse("f875eb1a-f930-49b3-5df6-08dae9686397");
            var result = asset.DownloadFile(Id);
            var finalresult = Assert.IsType<UnauthorizedResult>(result);

            
        }
    }
}
