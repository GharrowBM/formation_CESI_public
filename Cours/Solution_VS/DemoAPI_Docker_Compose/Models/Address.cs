using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GharrowDogsAPI.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        public int StreetNumber { get; set; }
        public string StreetName { get; set; }
        public int PostalCode { get; set; }
        public string CityName { get; set; }

        public virtual List<Master> Inhabitants { get; set; }
    }
}
