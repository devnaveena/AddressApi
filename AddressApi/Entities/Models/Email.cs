using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AddressApi
{
    public class Email
    {
        public DateTime? CreatedOn { get; set; }
       
        public bool? IsActive { get; set; }
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public Guid EmailId { get; set; }
        public string EmailAddress { get; set; }
        public Guid Type { get; set; }
     
    }
}