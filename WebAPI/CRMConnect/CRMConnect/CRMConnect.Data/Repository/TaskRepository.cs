using CRMConnect.CRMConnect.Data.DataAccess;
using CRMConnect.CRMConnect.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRMConnect.CRMConnect.Data.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;
        public TaskRepository(ApplicationDbContext context)
        {
            _context = context; 
        }

        public async Task<Core.Entities.Task> AddTaskDataAsync(Core.Entities.Task task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<bool> DeleteTaskDataAsync(int id)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
            if (task == null)
            {
                return false;
            }
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Core.Entities.Task>> GetAllTaskDataAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<Core.Entities.Task> GetTaskByIdDataAsync(int id)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(_ => _.Id == id);
            return task;
        }

        public async Task<bool> UpdateTaskDataAsync(Core.Entities.Task task)
        {
            var existingTask = await _context.Tasks.FirstOrDefaultAsync(t=>t.Id==task.Id);
            if(existingTask == null)
            {
                return false;
            }
            existingTask.Title = task.Title;
            existingTask.DueDate = task.DueDate;
            existingTask.Priority = task.Priority;
            existingTask.Status = task.Status;
            existingTask.AssignedTo = task.AssignedTo;
            existingTask.AssociatedEntityId = task.AssociatedEntityId;
            _context.Tasks.Update(existingTask);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
