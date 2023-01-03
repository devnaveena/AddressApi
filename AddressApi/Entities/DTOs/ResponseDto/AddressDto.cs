using System.ComponentModel.DataAnnotations;

namespace AddressApi.Entities.DTOs.ResponseDto
{
    public class AddressDto
    {
        [Required]

        public string Line1 { get; set; }

        [Required]
        public string Line2 { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public int ZipCode { get; set; }

        [Required]
        public string stateName { get; set; }

        [Required]
        public TypesDto Type { get; set; }

        [Required]
        public TypesDto country { get; set; }
    }
}
