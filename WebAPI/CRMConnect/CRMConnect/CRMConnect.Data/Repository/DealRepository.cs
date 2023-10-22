using CRMConnect.CRMConnect.Core.Entities;
using CRMConnect.CRMConnect.Data.DataAccess;
using CRMConnect.CRMConnect.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRMConnect.CRMConnect.Data.Repository
{
    public class DealRepository : IDealRepository
    {
        private readonly ApplicationDbContext _context;
        public DealRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Deal> AddDealDataAsync(Deal deal)
        {
            _context.Deals.Add(deal);
            await _context.SaveChangesAsync();
            return deal;
        }

        public async Task<bool> DeleteDealDataAsync(int id)
        {
            var deal = await _context.Deals.FirstOrDefaultAsync(d => d.id == id);
            if(deal == null)
            {
                return false;
            }
            _context.Deals.Remove(deal);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Deal>> GetAllDealsDataAsync()
        {
            return await _context.Deals
                .Include(account => account.Account)
                .Include(contact => contact.Contact)
                .Include(opportunity => opportunity.Opportunity).ToListAsync();
        }

        public async Task<Deal> GetDealByIdDataAsync(int id)
        {
            var deal = await _context.Deals
                .Include(account => account.Account)
                .Include(contact => contact.Contact)
                .Include(opportunity => opportunity.Opportunity).FirstOrDefaultAsync(d => d.id == id); 
            return deal;
        }

        public async Task<bool> UpdateDealDataAsync(Deal deal)
        {
            var existingDeal = await _context.Deals.FirstOrDefaultAsync(d => d.id == deal.id);
            if(existingDeal == null)
            {
                return false;
            }
            existingDeal.Name = deal.Name;  
            existingDeal.AccountId = deal.AccountId;
            existingDeal.ContactId = deal.ContactId;
            existingDeal.OpportunityId = deal.OpportunityId;   
            existingDeal.Amount = deal.Amount;
            existingDeal.ClosingDate = deal.ClosingDate;
            existingDeal.Stage = deal.Stage;
            _context.Deals.Update(existingDeal);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
