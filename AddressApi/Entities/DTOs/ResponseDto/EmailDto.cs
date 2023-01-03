using System.ComponentModel.DataAnnotations;

namespace AddressApi.Entities.DTOs.ResponseDto
{
    public class EmailDto
    {
        [EmailAddress]
        public string EmailAddress { get; set; }
        
        [Required]
        public TypesDto Type { get; set; }
    }
}
