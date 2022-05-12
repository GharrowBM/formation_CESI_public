using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GharrowDogsAPI.Models
{
    public class Dog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Breed { get; set; }

        public int? MasterId { get; set; }
        
        [ForeignKey("MasterId")]
        public virtual Master? Master { get; set; }
    }
}
