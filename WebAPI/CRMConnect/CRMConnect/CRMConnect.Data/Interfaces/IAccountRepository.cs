using CRMConnect.CRMConnect.Core.Entities;

namespace CRMConnect.CRMConnect.Data.Interfaces
{
    public interface IAccountRepository
    {
        Task<List<Account>> GetAllAccountsDataAsync();
        Task<Account> GetAccountDataByIdAsync(int id);
        Task<Account> AddAccountDataAsync(Account account);
        Task<bool> DeleteAccountDataAsync(int id);
        Task<bool> UpdateAccountDataAsync(Account account);
    }
}
