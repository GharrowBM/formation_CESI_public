using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GharrowDogsAPI.Models
{
    public class Master
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get => FirstName + " " + LastName; }
        public DateTime DateOfBirth { get; set; }
        public int Age
        {
            get
            {
                int age = 0;
                age = DateTime.Now.Year - DateOfBirth.Year;
                if (DateTime.Now.DayOfYear > DateOfBirth.DayOfYear) age--;
                return age;
            }
        }

        public int? AddressId { get; set; }
        
        [ForeignKey("AddressId")]
        public virtual Address? Address { get; set; }

        [JsonIgnore]
        public virtual List<Dog> Dogs { get; set; }
    }
}
