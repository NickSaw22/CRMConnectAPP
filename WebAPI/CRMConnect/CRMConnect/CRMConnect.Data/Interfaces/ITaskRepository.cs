using CRMConnect.CRMConnect.Core.Entities;
using Task = CRMConnect.CRMConnect.Core.Entities.Task;

namespace CRMConnect.CRMConnect.Data.Interfaces
{
    public interface ITaskRepository
    {
        Task<List<Task>> GetAllTaskDataAsync();
        Task<Task> GetTaskByIdDataAsync(int id);
        Task<Task> AddTaskDataAsync(Task task);
        Task<bool> DeleteTaskDataAsync(int id);
        Task<bool> UpdateTaskDataAsync(Task task);

    }
}
