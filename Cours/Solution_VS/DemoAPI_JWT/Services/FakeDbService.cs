using DemoAPI_JWT.Models;

namespace DemoAPI_JWT.Services
{
    public class FakeDbService
    {
        public List<Dog> Dogs { get; set; }
        public List<Master> Masters { get; set; }
        public List<Address> Addresses { get; set; }
        public List<Account> Accounts { get; set; }

        public FakeDbService()
        {
            Dogs = new();
            Masters = new();
            Addresses = new();
            Accounts = new();
        }

    }
}
