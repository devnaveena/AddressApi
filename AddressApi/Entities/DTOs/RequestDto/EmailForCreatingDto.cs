using AddressApi.Entities.DTOs.ResponseDto;

namespace AddressApi.Entities.DTOs.RequestDto
{
    public class EmailForCreatingDto :EmailDto
    {
        public Guid EmailId { get; set; }
    }
}
