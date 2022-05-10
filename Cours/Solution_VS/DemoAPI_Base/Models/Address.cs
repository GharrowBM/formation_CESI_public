namespace DemoAPI_Base.Models
{
    public class Address
    {
        public int Id { get; set; }
        public int StreetNumber { get; set; }
        public string StreetName { get; set; }
        public int PostalCode { get; set; }
        public string CityName { get; set; }

        public static int Count;
    }
}
