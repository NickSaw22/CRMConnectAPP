using CRMConnect.CRMConnect.Business.Interfaces;
using CRMConnect.CRMConnect.Core.Entities;
using CRMConnect.CRMConnect.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace CRMConnect.CRMConnect.Business.Implementaions
{
    public class OpportunityService : IOpportunityService
    {
        private readonly IOpportunityRepository _opportunityRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IContactRepository _contactRepository;
        private readonly IDealRepository _dealRepository;
        private readonly ILogStatusRepository _logStatusRepository;
        public OpportunityService(IOpportunityRepository opportunityRepository, IAccountRepository accountRepository, IContactRepository contactRepository, IDealRepository dealRepository, ILogStatusRepository logStatusRepository)
        {
            _opportunityRepository = opportunityRepository;
            _accountRepository = accountRepository;
            _contactRepository = contactRepository;
            _dealRepository = dealRepository;
            _logStatusRepository = logStatusRepository;
        }

        public Task<Opportunity> AddOpportunityAsync(Opportunity Opportunity)
        {
            Opportunity.CreatedById = Opportunity.AccountId;
            Opportunity.CreatedOn = DateTime.Now;
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

        public async Task<JObject> GetOpportunityStatusWiseAsync()
        {
            var opportunities = await _opportunityRepository.GetAllOpportunityDataAsync();

            if (opportunities == null || !opportunities.Any())
            {
                return JObject.FromObject(new
                {
                    label = new string[] { },
                    series = new int[] { }
                });
            }

            var statusCounts = opportunities
                .GroupBy(o => o.Stage)
                .Select(g => new
                {
                    Stage = g.Key,
                    Count = g.Count()
                })
                .OrderBy(sc => sc.Stage)
                .ToList();

            var labels = statusCounts.Select(sc => sc.Stage.ToString()).ToArray();
            var counts = statusCounts.Select(sc => sc.Count).ToArray();

            var result = new
            {
                label = labels,
                series = counts
            };

            return JObject.FromObject(result);
        }


        public async Task<bool> UpdateOpportunityAsync(Opportunity Opportunity)
        {
            var prevResp = await _opportunityRepository.GetOpportunityByIdDataAsync(Opportunity.Id);
            Opportunity.ModifiedById = Opportunity.AccountId;
            Opportunity.ModifiedOn = DateTime.Now;
            var result = await _opportunityRepository.UpdateOpportunityDataAsync(Opportunity);
            if (Opportunity.isStatusChanged == true)
            {
                LogStatus request = new LogStatus
                {
                    AssociatedEntity = "Opportunity",
                    EntityId = Opportunity.Id,
                    StatusFrom = ((int)prevResp.Stage),
                    StatusTo = ((int)Opportunity.Stage)
                };
                await _logStatusRepository.AddLogStatusAsync(request);
            }
            if (result != null && Opportunity.Stage == OpportunityStage.ClosedWon)
            {
                Deal deal = new Deal
                {
                    Name = "Deal - " + Opportunity.Name,
                    AccountId = Opportunity.AccountId,
                    ContactId = Opportunity.ContactId,
                    OpportunityId = Opportunity.Id,
                    Amount = Opportunity.Amount,
                    ClosingDate = Opportunity.ClosingDate,
                    Stage = DealStage.Open
                };

                await _dealRepository.AddDealDataAsync(deal);
            }
            return result;


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
