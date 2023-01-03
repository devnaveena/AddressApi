using AddressApi.Contracts;
using AddressApi.Entities.DTOs.RequestDto;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AddressApi.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AssetController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IJWTManagerRepository _jwtManagerRepository;
        private readonly ILogger<AssetController> _logger;
        private readonly ILog _log;

        public AssetController(IAccountService accountService, IJWTManagerRepository jwtManagerRepository, ILogger<AssetController> logger)
        {
            _accountService = accountService;
            _jwtManagerRepository = jwtManagerRepository;
            _logger = logger;
            _log = LogManager.GetLogger(typeof(AssetController));
        }
        /// <summary>
        /// File Upload API - stores a file in the database
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [Route("api/asset/uploadfile")]
        [HttpPost]
        public IActionResult UploadFile([FromQuery(Name = "user-id")] Guid userId, [FromForm] FileDto file)
        {
            Guid currentId = Guid.Parse("c572c99e-ee1f-4d17-b69c-08dae952ed26");
            //Guid currentId = _jwtManagerRepository.GetUserId();
            if (currentId != userId)
            {
                _log.Error($"User - {currentId}, is trying to access this User - {userId}");
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
            {
                UploadFileDto result = _accountService.UploadFile(file,userId);
                _log.Info("File Uploaded sucessfully ");

                return Ok( result);
            }
            
        }
        /// <summary>
        /// File Download API - retrives the  stored in the database
        /// </summary>
        /// <param name="assetId"></param>
        /// <returns></returns>
        [Route("api/asset")]
        [HttpGet]
        public IActionResult DownloadFile([FromQuery] Guid assetId)
        {
            Guid currentId = Guid.Parse("c572c99e-ee1f-4d17-b69c-08dae952ed26");
            // Guid currentId = _jwtManagerRepository.GetUserId();
            if (!_accountService.CheckAssetId(assetId, currentId))
            {
                _log.Error($"User - {currentId}, is trying to update this User - {assetId}");
                return Unauthorized();
            }
            Tuple<FileModel, string> file = _accountService.DownloadFile(assetId);
            if (file == null)
            {
                _log.Error($"User - not found in the database - {assetId}");
                return NotFound("File Not Found");
            }
            FileModel fileModel = (FileModel) file.Item1;
            var result= File(fileModel.file, fileModel.FileType);
            return Ok( result);
        }

    }

}

