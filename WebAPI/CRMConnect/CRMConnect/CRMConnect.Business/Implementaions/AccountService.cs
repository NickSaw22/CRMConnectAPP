using CRMConnect.CRMConnect.Business.Interfaces;
using CRMConnect.CRMConnect.Core.Entities;
using CRMConnect.CRMConnect.Data.DataAccess;
using CRMConnect.CRMConnect.Data.Interfaces;
using CRMConnect.CRMConnect.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace CRMConnect.CRMConnect.Business.Implementaions
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;


        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Account> CreateAccountAsync(Account account)
        {
            return await _accountRepository.AddAccountDataAsync(account);
        }

        public int GenerateId(List<Account> allAccounts)
        {
            return allAccounts.Count + 1;
        }

        public async Task<bool> DeleteAccountAsync(int id)
        {
            return await _accountRepository.DeleteAccountDataAsync(id);
        }

        public async Task<Account> GetAccountByIdAsync(int id)
        {
            return await _accountRepository.GetAccountDataByIdAsync(id);
        }

        public async Task<List<Account>> GetAllAccountsAsync()
        {
            return await _accountRepository.GetAllAccountsDataAsync();
        }

        public async Task<bool> UpdateAccountAsync(Account account)
        {
            return await _accountRepository.UpdateAccountDataAsync(account);
        }
    }
}
