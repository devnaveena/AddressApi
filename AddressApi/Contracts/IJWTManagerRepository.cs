using AddressApi.Entities.DTOs;
using AddressApi.Entities.DTOs.ResponseDto;

namespace AddressApi.Contracts
{
    public interface IJWTManagerRepository
    {
        /// <summary>
        ///Login the Valid User 
        /// </summary>
        /// <param name="signinDto">User</param>
        /// <returns></returns>
        public TokenDto Login(SiginInDto user);
        /// <summary>
        ///get the current User
        /// </summary>
        /// <returns></returns>
        public Guid GetUserId();
    }
}
