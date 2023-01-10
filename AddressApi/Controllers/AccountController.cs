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
using Newtonsoft.Json;

namespace AddressApi.Controllers
{

    [ApiController]
    
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IJWTManagerRepository _jWTManagerRepository;
        private readonly ILogger<AccountController> _logger;
        private readonly ILog _log;
        /// <summary>
        /// initalizes new instance for the class
        /// </summary>
        /// <param name="accountService">communicate between controller and repository</param>
        /// <param name="jWTManagerRepository"></param>
        /// <param name="logger"></param> 
        /// <returns>id of the user</returns>

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
        [HttpPost]
        public IActionResult CreateUser([FromBody] UserDto user)
        {
            _log.Info("Creating user in the database");
            try
            {
                _log.Info("Sending data to database" + user);
                var result = _accountService.Create(user);
                if (result == "true")
                {
                    _log.Debug($"Username already exists in the database");
                    return Conflict("Username already exists in the database");
                }
                if (result == "false")
                {
                    _log.Debug($"Email already exists in the database");
                    return Conflict("Email already exists in the database");
                }
                _log.Info("New User created with the user name: " + result);
                return Created("id of the user",result);

            }
            catch (KeyNotFoundException)
            {
                _log.Debug($"Given metadata not exists in the database");
                return NotFound();
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server error occurred" });
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
            _log.Info("Getting details from the database");

            PagedList<UserForCreatingDto> userToReturn = _accountService.GetAll(pagination);
            var metaData = new
            {
                totalCount = userToReturn.TotalCount,
                pageSize = userToReturn.PageSize,
                currentPage = userToReturn.CurrentPage,
                totalPages = userToReturn.TotalPages,
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metaData));

            if (userToReturn == null)
            {
                _log.Debug("No data found in the database");
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
            _log.Info($"geting, {userId}, details");
            Guid currentId = Guid.Parse("c572c99e-ee1f-4d17-b69c-08dae952ed26");
            //_jWTManagerRepository.GetUserId();
            try
            {
                UserForCreatingDto result = _accountService.GetUserbyId(userId);
                if (currentId != userId && result != null)
                {
                    _log.Debug($"User - {userId}, is trying to access  of the User - {userId}");
                    return Unauthorized();
                }
                if (result == null && currentId != userId)
                {
                    _log.Debug($"User - not found in the database - {userId}");
                    return NotFound("User Not Found in the database");
                }
                _log.Info($"User - {userId}, viewed the data");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _log.Error("Something went wrong", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }


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
            _log.Info("started counting the users");
            try
            {
                var result = _accountService.GetCount();
                _log.Info($"User , viewed the total number of Accounts ");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _log.Error("Something went wrong", ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

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
            _log.Info("Start to Update user deatils in the database");

            Guid currentId = Guid.Parse("c572c99e-ee1f-4d17-b69c-08dae952ed26");
            //Guid currentId = _jWTManagerRepository.GetUserId();

            if (currentId != id)
            {
                _log.Debug($"User - {id}, is trying to access  of the User - {currentId}");

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
                    _log.Debug($"User - not found in the database - {id}");
                    return NotFound();
                }
                _log.Info("User updated in the Account with the user name: " + userUp.UserName);
                return Ok(userUp);
            }
            catch (KeyNotFoundException)
            {
                _log.Debug($"Given metadata not exists in the database");

                return Conflict();
            }
            catch (Exception ex)
            {
                _log.Error("Something went wrong", ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
        /// <summary>
        /// Delete API - deletes an existing user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        [Route("api/account/{id}")]
        
        public  IActionResult DeleteUser(Guid id)
        {
            _log.Info("started to delete user deatils");
            Guid currentId = Guid.Parse("c572c99e-ee1f-4d17-b69c-08dae952ed26");
            // Guid currentId =  _jWTManagerRepository.GetUserId();
            if (currentId != id)
            {
                _log.Debug($"User - {currentId}, is trying to delete an Id of this User - {id}");
                return Unauthorized();
            }
            try
            {
                var result = _accountService.Delete(id);
                if (result == null)
                {
                    _log.Debug($"User - not found in the database - {id}");

                    return NotFound();
                }
                _log.Info("User deleted his account with the user id: " + id);
                return Ok("Address"+result+ "was deleted successfully");
            }

            catch (Exception ex)
            {
                _log.Error("Something went wrong", ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        /// <summary>
        /// Metadata  API 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>


        [HttpGet]
        [Route("api/meta/refset")]
        public ActionResult MetaData([FromQuery] string key)
        {
            _log.Info("entered into controller");
            var returnTo =_accountService.Metadata(key);
            return StatusCode(StatusCodes.Status200OK, returnTo);
        }
    }
}
