using CRMConnect.CRMConnect.Business.Interfaces;
using CRMConnect.CRMConnect.Core.Entities;
using CRMConnect.CRMConnect.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRMConnect.CRMConnect.Business.Implementaions
{
    public class OpportunityService : IOpportunityService
    {
        private readonly IOpportunityRepository _opportunityRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IContactRepository _contactRepository;
        public OpportunityService(IOpportunityRepository opportunityRepository, IAccountRepository accountRepository, IContactRepository contactRepository)
        {
            _opportunityRepository = opportunityRepository;
            _accountRepository = accountRepository;
            _contactRepository = contactRepository;
        }

        public Task<Opportunity> AddOpportunityAsync(Opportunity Opportunity)
        {
            return _opportunityRepository.AddOpportunityDataAsync(Opportunity);
        }

        public Task<bool> DeleteOpportunityAsync(int id)
        {
            return _opportunityRepository.DeleteOpportunityDataAsync(id);
        }

        public async Task<List<Opportunity>> GetAllOpportunityAsync()
        {
            var opportunities = _opportunityRepository.GetAllOpportunityDataAsync();            
            return await opportunities;
        }


        public async Task<Opportunity> GetOpportunityAsync(int id)
        {
            var opportunity = await _opportunityRepository.GetOpportunityByIdDataAsync(id);
            var accounts = await _accountRepository.GetAllAccountsDataAsync();
            var contacts = await _contactRepository.GetAllContactsDataAsync();
            AssociateOpportunityWithAccountAndContact(opportunity, accounts, contacts);
            return opportunity;
        }

        public async Task<bool> UpdateOpportunityAsync(Opportunity Opportunity)
        {
            return await _opportunityRepository.UpdateOpportunityDataAsync(Opportunity);
        }


        private List<Opportunity> AssociateOpportunitiesWithAccountsAndContacts(List<Opportunity> opportunities, List<Account> accounts, List<Contact> contacts)
        {
            return opportunities.Select(opportunity =>
            {
                AssociateOpportunityWithAccountAndContact(opportunity, accounts, contacts);
                return opportunity;
            }).ToList();
        }

        private void AssociateOpportunityWithAccountAndContact(Opportunity opportunity, List<Account> accounts, List<Contact> contacts)
        {
            if (opportunity.AccountId != null)
            {
                opportunity.Account = accounts.FirstOrDefault(acc => acc.Id == opportunity.AccountId);
            }

            if (opportunity.ContactId != null)
            {
                opportunity.Contact = contacts.FirstOrDefault(cont => cont.ContactId == opportunity.ContactId);
            }
        }
    }
}
