using System.ComponentModel.DataAnnotations;

namespace AddressApi.Entities.DTOs.ResponseDto
{
    public class TypesDto
    {
        [Required]
        public string Key { get; set; }
    }
}
