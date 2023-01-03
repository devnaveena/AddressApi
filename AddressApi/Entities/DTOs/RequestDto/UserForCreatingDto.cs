namespace AddressApi.Entities.DTOs.RequestDto
{
    public class UserForCreatingDto
    {
        public Guid Id { get; set; }
       
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
       

        public List<EmailForCreatingDto>? Email { get; set; }
        

        public List<AddressForCreatingDto>? Address { get; set; }
        

        public List<PhoneForCreatingDto> Phone { get; set; }
    }
}
