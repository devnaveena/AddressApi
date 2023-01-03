using AddressApi.Entities.DTOs.ResponseDto;

namespace AddressApi.Entities.DTOs.RequestDto
{
    public class AddressForCreatingDto :AddressDto
    {
        public Guid AddressId { get; set; }
    }
}
