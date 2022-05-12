using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _07_eBooks.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public int NbOfPages { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string CoverURL { get; set; }
        
        public int? EditorId { get; set; }

        [ForeignKey("EditorId")]
        public virtual Editor? Editor { get; set; }
        public int? AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public virtual Author? Author { get; set; }
    }
}
