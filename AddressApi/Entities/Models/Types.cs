using System.ComponentModel.DataAnnotations;

namespace AddressApi
{
    public class Types
    {
        [Key]
        public Guid RefsetType { get; set; }
        public string Key { get; set; } 
    }
}
