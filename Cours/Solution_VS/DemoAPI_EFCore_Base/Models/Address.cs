using System.ComponentModel.DataAnnotations;

namespace DemoAPI_EFCore.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

        public int StreetNumber { get; set; }
        
        [StringLength(150)]
        public string StreetName { get; set; }
        
        public int PostalCode { get; set; }
        
        [StringLength(50)]
        public string CityName { get; set; }
    }
}
