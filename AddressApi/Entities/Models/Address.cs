
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AddressApi
{
    public class Address
    {
        public DateTime? CreatedOn { get; set; }
       
        public bool? IsActive { get; set; }
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        [Key]
        public Guid AddressId { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
        public string State { get; set; }
        public Guid Type { get; set; }
        public Guid country { get; set; }
        
    }
}
