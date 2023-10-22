using CRMConnect.CRMConnect.Core.Entities;
using CRMConnect.CRMConnect.Data.DataAccess;
using CRMConnect.CRMConnect.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRMConnect.CRMConnect.Data.Repository
{
    public class OpportunityRepository : IOpportunityRepository
    {
        private readonly ApplicationDbContext _context;
        public OpportunityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Opportunity> AddOpportunityDataAsync(Opportunity opportunity)
        {
            _context.Opportunity.Add(opportunity);
            await _context.SaveChangesAsync();
            return opportunity;
        }

        public async Task<bool> DeleteOpportunityDataAsync(int id)
        {
            var result = _context.Opportunity.FirstOrDefaultAsync(x => x.Id == id);
            if(result == null)
            {
                return false;
            }
            _context.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Opportunity>> GetAllOpportunityDataAsync()
        {
            return await _context.Opportunity.Include(opportunity => opportunity.Account)
        .Include(opportunity => opportunity.Contact)
        .ToListAsync(); 
        }

        public Task<Opportunity> GetOpportunityByIdDataAsync(int id)
        {
            var result = _context.Opportunity.FirstOrDefaultAsync(_ => _.Id == id);            
            return result;
        }

        public async Task<bool> UpdateOpportunityDataAsync(Opportunity opportunity)
        {
            var existingOpp = await _context.Opportunity.FirstOrDefaultAsync(o => o.Id == opportunity.Id);
            if(existingOpp == null)
            {
                return false;
            }
            existingOpp.Name = opportunity.Name;
            existingOpp.AccountId = opportunity.AccountId;
            existingOpp.ContactId = opportunity.ContactId;
            existingOpp.Stage = opportunity.Stage;
            existingOpp.Amount = opportunity.Amount;
            existingOpp.ClosingDate = opportunity.ClosingDate;
            _context.Opportunity.Update(existingOpp);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
