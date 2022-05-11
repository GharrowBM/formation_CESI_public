using System.Text.Json.Serialization;

namespace TP05.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public virtual List<Pizza> Pizza { get; set; }
    }
}
