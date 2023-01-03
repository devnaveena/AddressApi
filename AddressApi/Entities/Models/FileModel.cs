using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AddressApi
{
    public partial class FileModel
    {


        [Key]
        public Guid Id { get; set; }

        public byte[] file { get; set; }

        public string FileType { get; set; } = string.Empty;


        [ForeignKey("UserId")]
        public User User { get; set; }
        public Guid UserId { get; set; }


    }
}
