namespace DemoAPI_JWT.Models
{
    public class Dog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public int MasterId { get; set; }
        public Master Master { get; set; }

        public static int Count;
    }
}
