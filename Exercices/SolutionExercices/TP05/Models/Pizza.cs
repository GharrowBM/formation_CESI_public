namespace TP05.Models
{
    public class Pizza
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public bool IsVegan { get; set; }
        public bool IsSpicy { get; set; }
        public string PictureURL { get; set; }
        public virtual List<Ingredient> Ingredients { get; set; }
    }
}
