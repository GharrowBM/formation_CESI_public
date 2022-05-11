using System.ComponentModel.DataAnnotations;

namespace _03_Demenagements.Models
{
    public class Property
    {
        [Key]
        public int Id { get; set; }

        public int StreetNumber { get; set; }

        [StringLength(100)]
        public string StreetName { get; set; }

        public int PostalCode { get; set; }

        [StringLength(100)]
        public string CityName { get; set; }

        public virtual List<Person>? Inhabitants { get; set; }
    }
}
