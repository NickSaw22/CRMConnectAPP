using CRMConnect.CRMConnect.Business.Interfaces;
using CRMConnect.CRMConnect.Core.Entities;
using CRMConnect.CRMConnect.Data.Interfaces;

namespace CRMConnect.CRMConnect.Business.Implementaions
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public Account CreateAccount(Account account)
        {
            var allAccounts = _accountRepository.GetAllAccounts();
            account.Id = GenerateId(allAccounts);
            allAccounts.Add(account);
            return account;
        }

        public int GenerateId(List<Account> allAccounts)
        {
            return allAccounts.Count + 1;
        }

        public bool DeleteAccount(int id)
        {
            var allaccounts = _accountRepository.GetAllAccounts();
            var result = allaccounts.FirstOrDefault(a => a.Id == id);
            if(result == null)
            {
                return false;
            }
            return true;
        }

        public Account GetAccountById(int id)
        {
            var allAccounts = _accountRepository.GetAllAccounts();
            var result = allAccounts.FirstOrDefault(a => a.Id == id);
            return result;
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            var result = _accountRepository.GetAllAccounts();
            return result;
        }

        public bool UpdateAccount(Account account)
        {
            var allAccounts = _accountRepository.GetAllAccounts();
            var result = allAccounts.FirstOrDefault(a => a.Id == account.Id);
            if( result == null)
            {
                return false;
            }
            result.Name = account.Name;
            result.Address = account.Address;
            result.PhoneNumber = account.PhoneNumber;
            result.Email = account.Email;
            return true;
        }
    }
}
