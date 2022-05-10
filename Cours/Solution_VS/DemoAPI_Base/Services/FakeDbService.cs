using DemoAPI_Base.Models;

namespace DemoAPI_Base.Services
{
    public class FakeDbService
    {
        public List<Dog> Dogs { get; set; }
        public List<Master> Masters { get; set; }
        public List<Address> Addresses { get; set; }

        public FakeDbService()
        {
            Dogs = new();
            Masters = new();
            Addresses = new();
        }

    }
}
