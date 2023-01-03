using AddressApi.Contracts;
using AddressApi.Entities.DTOs.RequestDto;
using AddressApi.Entities.DTOs.ResponseDto;
using AddressApi.Entities.Helper;
using AddressApi.Service;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AddressApi.Controllers
{

    [ApiController]
    
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IJWTManagerRepository _jWTManagerRepository;
        private readonly ILogger<AccountController> _logger;
        private readonly ILog _log;
        public AccountController(IAccountService accountService, IJWTManagerRepository jWTManagerRepository, ILogger<AccountController> logger)
        {
            _accountService = accountService;
            _jWTManagerRepository = jWTManagerRepository;
            _log = LogManager.GetLogger(typeof(AccountController));
            _logger = logger;
        }
        /// <summary>
        /// Create a new User in the database
        /// </summary>
        /// <param name="user"></param>
        /// <returns>id of the user</returns>

        [AllowAnonymous]
        [Route("api/account")]
        public IActionResult CreateUser([FromBody] UserDto user)
        {
            try
            {
                var result = _accountService.Create(user);
                if (result == "true")
                {
                    _log.Error($"Username already exists in the database");
                    return Conflict("Username already exists in the database");
                }
                if (result == "false")
                {
                    _log.Error($"Email already exists in the database");
                    return Conflict("Email already exists in the database");
                }
                _log.Info("New User created with the user name: " + result);
                return Ok(result);

            }
            catch (KeyNotFoundException)
            {
                _log.Error($"Given metadata not exists in the database");
                return NotFound("MetadataNotFound");
            }

        }
        /// <summary>
        /// for getting all the user from the database
        /// </summary>
        /// <returns></returns>
        // get all the user
        [HttpGet]
        [Route("api/account")]
        [AllowAnonymous]

        public IActionResult GetAllUser([FromQuery] Pagination pagination)
        {

            var userToReturn = _accountService.GetAll(pagination);
            if (userToReturn == null)
            {
                return NoContent();
            }
            _log.Info("Gets all users in the database: " +userToReturn);
            return Ok(userToReturn);


        }
        /// <summary>
        /// Get API - gets an user by id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>User details by their ID</returns>
        [HttpGet]
        [Authorize]
        [Route("api/account/{userId:guid}")]

        public IActionResult GetById(Guid userId)
        {
            Guid currentId = Guid.Parse("c572c99e-ee1f-4d17-b69c-08dae952ed26");
            //_jWTManagerRepository.GetUserId();
            

            UserForCreatingDto result = _accountService.GetUserbyId(userId);
            if (currentId != userId && result!=null)
            {
                _log.Error($"User - {userId}, is trying to access  of the User - {userId}");
                return Unauthorized();
            }
            if (result == null && currentId != userId)
            {
                _log.Error($"User - not found in the database - {userId}");
                return NotFound("User Not Found in the database");
            }
            _log.Info($"User - {userId}, viewed the data");
            return Ok(result);
            

        }
        /// <summary>
        /// Count API - counts users in the database
        /// </summary>
        /// <returns>count the users in the database</returns>
        [HttpGet]
        [Authorize]
        [Route("api/account/count")]

        public IActionResult CountUsers()

        { 
            var result = _accountService.GetCount();
           _log.Info($"User , viewed the total number of Accounts ");
            return Ok(result);
        }
        /// <summary>
        /// Update API - updates the existing user 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userupdate"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/account/{id}")]
        [Authorize]
        public IActionResult Update(Guid id,[FromBody] UserDto userupdate)
        {
            Guid currentId = Guid.Parse("c572c99e-ee1f-4d17-b69c-08dae952ed26");
            //Guid currentId = _jWTManagerRepository.GetUserId();

            if (currentId != id)
            {
                _log.Error($"User - {id}, is trying to access  of the User - {currentId}");

                return Unauthorized();
            }
            bool checkUserExists = _accountService.UserExist(id, userupdate);

            if (checkUserExists)
                return Conflict( "Username already exists in the database");
            UpdateDto userUp = new UpdateDto();
            try
            {
                userUp = (UpdateDto)_accountService.Update(id, userupdate);
                if (userUp == null)
                {
                    _log.Error($"User - not found in the database - {id}");
                    return NotFound("User Not Found in the database");
                }
                _log.Info("User updated in the Account with the user name: " + userUp.UserName);
                return Ok(userUp);
            }
            catch (KeyNotFoundException)
            {
                _log.Error($"Given metadata not exists in the database");

                return Conflict( "Given Metadata Not valid");
            }

        }
        /// <summary>
        /// Delete API - deletes an existing user by id
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        [Route("api/account/{id}")]
        
        public  IActionResult DeleteUser(Guid id)
        {
            Guid currentId = Guid.Parse("c572c99e-ee1f-4d17-b69c-08dae952ed26");
            // Guid currentId =  _jWTManagerRepository.GetUserId();
            if (currentId != id)
            {
                _log.Error($"User - {currentId}, is trying to delete an Id of this User - {id}");
                return Unauthorized();
            }
            var result = _accountService.Delete(id);
            if (result == null)
            {
                _log.Error($"User - not found in the database - {id}");

                return NotFound("User Not Found");
            }
            _log.Info("User deleted his account with the user id: " + id);
            return Ok(result);
            
           
        }
        /// <summary>
        /// Metadata  API - deletes an existing user by id
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>


        [HttpGet]

        [Route("api/meta/refset")]
        public ActionResult MetaData([FromQuery] string key)
        {

            var returnTo =_accountService.Metadata(key);
            return StatusCode(StatusCodes.Status200OK, returnTo);
        }
    }
}
