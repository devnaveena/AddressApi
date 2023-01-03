using System.ComponentModel.DataAnnotations;

namespace AddressApi.Entities.DTOs
{
    public class SiginInDto
    {

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
       
    }
}
