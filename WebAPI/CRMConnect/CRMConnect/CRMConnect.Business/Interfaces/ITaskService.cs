using CRMConnect.CRMConnect.Core.Entities;
using Task = CRMConnect.CRMConnect.Core.Entities.Task;

namespace CRMConnect.CRMConnect.Business.Interfaces
{
    public interface ITaskService
    {
        Task<List<Task>> GetAllTasksAsync();
        Task<Task> GetTaskAsync(int id);
        Task<bool> DeleteTaskAsync(int id);
        Task<Task> AddTaskAsync(Task Task);
        Task<bool> UpdateTaskAsync(Task Task);
    }
}
