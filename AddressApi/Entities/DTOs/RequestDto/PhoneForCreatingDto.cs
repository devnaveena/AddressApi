using AddressApi.Entities.DTOs.ResponseDto;

namespace AddressApi.Entities.DTOs.RequestDto
{
    public class PhoneForCreatingDto : PhoneNumberDto
    {
        public Guid PhoneNumberId { get; set; }
    }
}
