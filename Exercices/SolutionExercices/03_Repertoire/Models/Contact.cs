using System.ComponentModel.DataAnnotations;

namespace _03_Repertoire.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }
        public Gender Gender { get; set; }

        public string Fullname
        {
            get
            {
                return Firstname + " " + Lastname;
            }
        }

        [Required]
        public string Password { get; set; }

        public string Phone { get; set; }

        [Required]
        public string Email { get; set; }

    }

    public enum Gender
    {
        Male,
        Female,
        NonBinary
    }
}
