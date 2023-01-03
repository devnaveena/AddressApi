using System.ComponentModel.DataAnnotations;

namespace AddressApi.Entities.DTOs.ResponseDto
{
    public class UserDto
    {
       
        [Required]
        public string UserName { get; set; }
       
        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public List<EmailDto>? Email { get; set; }     
        [Required]
        public List<AddressDto>? Address { get; set; }
        [Required]
        public List<PhoneNumberDto> phones { get; set; }
    }
}
