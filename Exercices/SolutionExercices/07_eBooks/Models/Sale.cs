using System.ComponentModel.DataAnnotations;

namespace _07_eBooks.Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateOfSale { get; set; }
        public decimal TotalValue { get; set; }
        public virtual List<Book> Books { get; set; }
    }
}
