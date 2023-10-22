using CRMConnect.CRMConnect.Core.Entities;
using CRMConnect.CRMConnect.Data.DataAccess;
using CRMConnect.CRMConnect.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRMConnect.CRMConnect.Data.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _context;

        public AccountRepository(ApplicationDbContext context) 
        { 
            _context = context;
        }

        public async Task<Account> AddAccountDataAsync(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();  
            return account;
        }

        public async Task<bool> DeleteAccountDataAsync(int id)
        {
            var accountToDelete = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == id);
            if (accountToDelete == null)
            {
                return false;
            }
            _context.Accounts.Remove(accountToDelete);
            return true;
        }

        public async Task<Account> GetAccountDataByIdAsync(int id)
        {
            var result =  await _context.Accounts.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task<List<Account>> GetAllAccountsDataAsync()
        {
            var accounts = await _context.Accounts.ToListAsync();
            return accounts;
        }

        public async Task<bool> UpdateAccountDataAsync(Account account)
        {
            var existingAccount = await _context.Accounts.FirstOrDefaultAsync(a=>a.Id == account.Id);
            if(existingAccount == null)
            {
                return false;
            }
            existingAccount.Name = account.Name;
            existingAccount.Email = account.Email;  
            existingAccount.Address = account.Address;
            existingAccount.PhoneNumber = account.PhoneNumber;
            _context.Accounts.Update(existingAccount);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
