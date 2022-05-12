using System.ComponentModel.DataAnnotations;

namespace _07_eBooks.Models
{
    public class Editor
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get => FirstName + " " + LastName; }
        public Gender Gender { get; set; }
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

        public virtual List<Book> Books { get; set; }
    }
}
