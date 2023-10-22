using CRMConnect.CRMConnect.Business.Interfaces;
using CRMConnect.CRMConnect.Core.Entities;
using CRMConnect.CRMConnect.Data.Interfaces;

namespace CRMConnect.CRMConnect.Business.Implementaions
{
    public class DealService : IDealService
    {
        private readonly IDealRepository _dealRepository;        
        public DealService(IDealRepository dealRepository)
        {
            _dealRepository = dealRepository;
        }

        public Task<Deal> AddDealAsync(Deal Deal)
        {
            return _dealRepository.AddDealDataAsync(Deal);
        }

        public Task<bool> DeleteDealAsync(int id)
        {
            return _dealRepository.DeleteDealDataAsync(id);
        }

        public Task<List<Deal>> GetAllDealsAsync()
        {
            return _dealRepository.GetAllDealsDataAsync();
        }

        public async Task<Deal> GetDealAsync(int id)
        {
            return await _dealRepository.GetDealByIdDataAsync(id);
        }

        public async Task<bool> UpdateDealAsync(Deal Deal)
        {
            return await _dealRepository.UpdateDealDataAsync(Deal);
        }
    }
}
