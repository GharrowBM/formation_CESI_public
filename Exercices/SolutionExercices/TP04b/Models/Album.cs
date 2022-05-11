using System.ComponentModel.DataAnnotations;

namespace TP04b.Models
{
    public class Album
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public string CoverURL { get; set; }

        public static int Count;
    }
}
