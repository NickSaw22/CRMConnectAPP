using CRMConnect.CRMConnect.Core.Entities;

namespace CRMConnect.CRMConnect.Data.Interfaces
{
    public interface IOpportunityRepository
    {
        Task<List<Opportunity>> GetAllOpportunityDataAsync();
        Task<Opportunity> GetOpportunityByIdDataAsync(int id);
        Task<Opportunity> AddOpportunityDataAsync(Opportunity opportunity);
        Task<bool> DeleteOpportunityDataAsync(int id);
        Task<bool> UpdateOpportunityDataAsync(Opportunity opportunity);
    }
}
