using CRMConnect.CRMConnect.Core.Entities;

namespace CRMConnect.CRMConnect.Business.Interfaces
{
    public interface IDealService
    {
        Task<List<Deal>> GetAllDealsAsync();
        Task<Deal> GetDealAsync(int id);
        Task<bool> DeleteDealAsync(int id);
        Task<Deal> AddDealAsync(Deal Deal);
        Task<bool> UpdateDealAsync(Deal Deal);
    }
}
