using CRMConnect.CRMConnect.Business.Interfaces;
using CRMConnect.CRMConnect.Core.Entities;
using CRMConnect.CRMConnect.Data.Interfaces;
using Task = CRMConnect.CRMConnect.Core.Entities.Task;

namespace CRMConnect.CRMConnect.Business.Implementaions
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public Task<Task> AddTaskAsync(Task Task)
        {
            return _taskRepository.AddTaskDataAsync(Task);
        }

        public Task<bool> DeleteTaskAsync(int id)
        {
            return _taskRepository.DeleteTaskDataAsync(id);
        }

        public Task<List<Task>> GetAllTasksAsync()
        {
            return _taskRepository.GetAllTaskDataAsync();
        }

        public async Task<Task> GetTaskAsync(int id)
        {
            return await _taskRepository.GetTaskByIdDataAsync(id);
        }

        public async Task<bool> UpdateTaskAsync(Task Task)
        {
            return await _taskRepository.UpdateTaskDataAsync(Task);
        }
    }
}
