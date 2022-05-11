using System.ComponentModel.DataAnnotations;

namespace _03_Repertoire.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Firstname { get; set; }

        [Required]
        [StringLength(100)]

        public string Lastname { get; set; }
        public Gender Gender { get; set; }

        public string Fullname
        {
            get
            {
                return Firstname + " " + Lastname;
            }
        }

        [StringLength(20)]
        public string Phone { get; set; }

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

        [Required]
        [StringLength(100)]

        public string Email { get; set; }

    }

    public enum Gender
    {
        Male,
        Female,
        NonBinary
    }
}
