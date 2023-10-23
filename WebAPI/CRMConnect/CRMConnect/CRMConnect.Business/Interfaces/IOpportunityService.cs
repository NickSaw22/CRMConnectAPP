using CRMConnect.CRMConnect.Core.Entities;
using Newtonsoft.Json.Linq;
using System.Diagnostics.CodeAnalysis;

namespace CRMConnect.CRMConnect.Business.Interfaces
{
    public interface IOpportunityService
    {
        Task<List<Opportunity>> GetAllOpportunityAsync();
        Task<Opportunity> GetOpportunityAsync(int id);
        Task<bool> DeleteOpportunityAsync(int id);
        Task<Opportunity> AddOpportunityAsync(Opportunity Opportunity);
        Task<bool> UpdateOpportunityAsync(Opportunity Opportunity);
        Task<JObject> GetOpportunityStatusWiseAsync();
    }
}
