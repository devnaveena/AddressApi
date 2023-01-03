using AddressApi.Entities.DTOs.ResponseDto;
using System.ComponentModel.DataAnnotations;

namespace AddressApi.Entities.DTOs.RequestDto
{
    public class UpdateDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public List<EmailDto> Email { get; set; }
        [Required]
        public List<AddressDto> Address { get; set; }
        [Required]
        public List<PhoneNumberDto> phoneNumber { get; set; }
    }
}
