using AddressApi.Contracts;
using AddressApi.Entities.DTOs;
using AddressApi.Entities.DTOs.ResponseDto;
using AddressApi.Repository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AddressApi.Service
{
    public class JWTManagerRepository : IJWTManagerRepository
    {


		private readonly RepositoryContext _context;
		private readonly IConfiguration _configuration;
		private readonly IHttpContextAccessor _httpContextAccessor;
		public JWTManagerRepository(RepositoryContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Authenticates the user with the given username and password and returns token upon successful authentication
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public TokenDto Login(SiginInDto user)
		{
			string password = _context.User.Where(a => a.UserName == user.UserName).Select(a => a.Password).SingleOrDefault();
			if (password == null || password != user.Password)
			{
				return null;
			}


			Guid currentId =_context.User.Where(a => a.UserName == user.UserName).Select(a => a.Id).SingleOrDefault();
			var tokenHandler = new JwtSecurityTokenHandler();
			var tokenKey = Encoding.UTF8.GetBytes("My secrt for my application");
			//var tokenIssuer=
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
			  {
				new Claim("id", currentId.ToString()),


			  }),
				Expires = DateTime.UtcNow.AddMinutes(10),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return new TokenDto { accessToken = tokenHandler.WriteToken(token), tokenType = "Bearer" };



		}
		public Guid GetUserId()
		{
			Guid UserId = Guid.Parse(_httpContextAccessor.HttpContext.User.Claims.First(i => i.Type == "id").Value);
			return UserId;
		}
	}
}
