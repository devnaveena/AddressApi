using System.ComponentModel.DataAnnotations;

namespace AddressApi.Entities.DTOs.ResponseDto
{
    public class PhoneNumberDto
    {
        
        public string PhoneNumber { get; set; }
        
        [Required]
        public TypesDto Type { get; set; }
    }
}
