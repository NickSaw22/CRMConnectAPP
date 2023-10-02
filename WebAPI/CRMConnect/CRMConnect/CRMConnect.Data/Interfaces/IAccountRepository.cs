using CRMConnect.CRMConnect.Core.Entities;

namespace CRMConnect.CRMConnect.Data.Interfaces
{
    public interface IAccountRepository
    {
        List<Account> GetAllAccounts();
    }
}
