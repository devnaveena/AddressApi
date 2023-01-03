using System.ComponentModel.DataAnnotations;

namespace AddressApi.Entities.DTOs.RequestDto
{
    public class FileDto
    {
        [Required]
        public IFormFile file { get; set; }
    }
}
