namespace AddressApi.Entities.DTOs.RequestDto
{
    public class UploadFileDto
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string FileName { get; set; } = string.Empty;

        public string FileType { get; set; } = string.Empty;
        public string? DownloadUrl { get; set; }

    }
}
