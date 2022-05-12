using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace _06_Librarie.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        [StringLength(50)]
        public string ISBN { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal Price { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? Editor { get; set; }
        public string? Author { get; set; }
        
        [Required]
        public int Quantity { get; set; }
        public Score? Score { get; set; }
    }

    public enum Score
    {
        Worst,
        Bad,
        Normal,
        Good,
        Best
    }
}
