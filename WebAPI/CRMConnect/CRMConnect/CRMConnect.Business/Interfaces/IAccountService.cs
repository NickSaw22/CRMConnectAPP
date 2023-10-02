using CRMConnect.CRMConnect.Core.Entities;

namespace CRMConnect.CRMConnect.Business.Interfaces
{
    public interface IAccountService
    {
        IEnumerable<Account> GetAllAccounts();
        Account GetAccountById(int id);
        Account CreateAccount(Account account);
        bool UpdateAccount(Account account);
        bool DeleteAccount(int id);
    }
}
