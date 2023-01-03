using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace AddressApi
{
    public class PhoneNumber
    {
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        [Key]
        public Guid phoneId { get; set; }
        public string PhoneNo { get; set; }
      
        public Guid Type { get; set; }
        public DateTime? CreatedOn { get; set; }
        
        public bool? IsActive { get; set; }
    }
}
