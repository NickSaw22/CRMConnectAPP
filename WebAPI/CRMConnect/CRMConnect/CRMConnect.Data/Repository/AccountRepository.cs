using CRMConnect.CRMConnect.Core.Entities;
using CRMConnect.CRMConnect.Data.Interfaces;

namespace CRMConnect.CRMConnect.Data.Repository
{
    public class AccountRepository : IAccountRepository
    {
        public AccountRepository() { }

        public List<Account> GetAllAccounts()
        {
            var result = new List<Account>
            {
                new Account { Id = 1, Name = "ABC Inc", Address = "123 Main St", PhoneNumber = "555-123-4567", Email = "abc@example.com" },
                new Account { Id = 2, Name = "XYZ Corp", Address = "456 Elm St", PhoneNumber = "555-987-6543", Email = "xyz@example.com" }
            };
            return result;
        }
    }
}
