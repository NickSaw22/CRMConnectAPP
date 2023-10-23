using CRMConnect.CRMConnect.Core.Entities;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;

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
