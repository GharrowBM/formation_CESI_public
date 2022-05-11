using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace _03_Demenagements.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }
        
        [StringLength(50)]
        public string LastName { get; set; }
        
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }
        
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [JsonIgnore]
        public virtual List<Property>? Properties { get; set; }
    }
}
