using AddressApi.Contracts;
using AddressApi.Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AddressApi.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJWTManagerRepository _jWTManagerRepository;

        public AuthController(IJWTManagerRepository jWTManagerRepository)
        {

            _jWTManagerRepository = jWTManagerRepository;
        }
		/// <summary>
		/// Login API
		/// </summary>
		/// <param name="signinDto"></param>
		/// <returns></returns>

		[HttpPost]
		[Route("api/auth/signin")]
		public IActionResult Authenticate(SiginInDto signinDto)
		{
			var token = _jWTManagerRepository.Login(signinDto);

			if (token == null)
			{
				return Unauthorized();
			}

			return Ok( token);
		}
	}
}

