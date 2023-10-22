using CRMConnect.CRMConnect.Business.Implementaions;
using CRMConnect.CRMConnect.Business.Interfaces;
using CRMConnect.CRMConnect.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using Task = CRMConnect.CRMConnect.Core.Entities.Task;

namespace CRMConnect.CRMConnect.Service.Controllers
{
    [Route("api/Tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        public TaskController(ITaskService taskService) 
        {
            _taskService = taskService;
        }

        [HttpGet("getAllTasks")]
        public async Task<IActionResult> GetAllTasks() 
        {
            var result = await _taskService.GetAllTasksAsync();
            return Ok(result);
        }

        [HttpGet("getTask/{id}")]
        public async Task<IActionResult> GetTask(int id)
        {
            var result = await _taskService.GetTaskAsync(id);
            return Ok(result);
        }

        [HttpPost("createTask")]
        public async Task<IActionResult> AddTask([FromBody] Task request)
        {
            if(request == null)
            {
                return BadRequest(string.Empty);
            }
            var result = await _taskService.AddTaskAsync(request);
            return Ok(result);
        }


        [HttpDelete("deleteTask/{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var result = await _taskService.DeleteTaskAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost("updateTask")]
        public async Task<IActionResult> UpdateTask([FromBody] Task request)
        {
            if(request == null)
            {
                return BadRequest(string.Empty);
            }
            var result = await _taskService.UpdateTaskAsync(request);
            return Ok(result);
        }
    }
}
