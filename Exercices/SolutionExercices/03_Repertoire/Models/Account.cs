using System.ComponentModel.DataAnnotations;

namespace _03_Repertoire.Models
{
    public class Account
    {
        public int Id { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9_-]{3,16}$")]
        public string UserName { get; set; }

        [RegularExpression(@"(?=(.*[0-9]))((?=.*[A-Za-z0-9])(?=.*[A-Z])(?=.*[a-z]))^.{8,}$")]
        public string Password { get; set; }

        [EmailAddress]
        public string EmailAddress { get; set; }
        public bool IsAdmin { get; set; }

        public static int Count;
    }
}
