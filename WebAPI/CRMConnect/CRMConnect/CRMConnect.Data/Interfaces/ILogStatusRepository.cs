using CRMConnect.CRMConnect.Core.Entities;

namespace CRMConnect.CRMConnect.Data.Interfaces
{
    public interface ILogStatusRepository
    {
        Task<List<LogStatus>> GetLogStatusAsync();
        Task<LogStatus> AddLogStatusAsync(LogStatus logStatus);
    }
}
