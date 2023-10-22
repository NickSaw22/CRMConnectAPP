using CRMConnect.CRMConnect.Core.Entities;

namespace CRMConnect.CRMConnect.Data.Interfaces
{
    public interface IDealRepository
    {
        Task<List<Deal>> GetAllDealsDataAsync();
        Task<Deal> GetDealByIdDataAsync(int id);
        Task<Deal> AddDealDataAsync(Deal deal);
        Task<bool> UpdateDealDataAsync(Deal deal);
        Task<bool> DeleteDealDataAsync(int id);
    }
}
