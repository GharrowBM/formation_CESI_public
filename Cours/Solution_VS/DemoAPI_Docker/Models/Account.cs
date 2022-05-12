using System.ComponentModel.DataAnnotations;

namespace DemoAPI_Docker.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9_-]{3,16}$")]
        [StringLength(50)]
        public string UserName { get; set; }

        [RegularExpression(@"(?=(.*[0-9]))((?=.*[A-Za-z0-9])(?=.*[A-Z])(?=.*[a-z]))^.{8,}$")]
        [StringLength(50)]
        public string Password { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string EmailAddress { get; set; }
        public bool IsAdmin { get; set; }
    }
}
