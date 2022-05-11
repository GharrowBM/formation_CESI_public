using TP04b.Models;

namespace TP04b.Services
{
    public class FakeDbService
    {
        public List<Album> Albums { get; set; }
        public List<Account> Accounts { get; set; }

        public FakeDbService()
        {
            Albums = new();
            Accounts = new();
        }
    }
}
