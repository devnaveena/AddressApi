
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AddressApi
{
    public class User
    {
        public User()
        {
        }
        [Key]
        public Guid Id { get; set; }    
        public string UserName { get; set; }    
        public string Password { get; set; }    
        public string FirstName { get; set; }
        public string LastName { get; set; }
     
        public List<Email>? Email { get; set; }
        
        public List<Address>? Address { get; set; }
     
        public List<PhoneNumber>? PhoneNumber { get; set; }
        public DateTime? CreatedOn { get; set; }
        
        public bool? IsActive { get; set; }=true;   
    }
}