using System.ComponentModel.DataAnnotations;

namespace AddressApi
{
    public class RefSet
    {
        [Key]
        public Guid RefSetId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
       

    }
}
