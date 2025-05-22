using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using csharp_demo_api.Domain.Entities;
using csharp_demo_api.Application.Services;

namespace csharp_demo_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController(TaskService service) : ControllerBase
    {
        private readonly TaskService _service = service;

        [HttpGet]
        public async Task<IEnumerable<TaskEntity>> GetAll()
        {
            return await _service.GetAllTasksAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskEntity>> GetById(Guid id)
        {
            var task = await _service.GetTaskByIdAsync(id);
            if (task == null) return NotFound();
            return task;
        }

        [HttpPost]
        public async Task<ActionResult<TaskEntity>> Create([FromBody] TaskEntity task)
        {
            await _service.AddTaskAsync(task);
            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] TaskEntity updatedTask)
        {
            if (id != updatedTask.Id) return BadRequest("ID mismatch");

            var existing = await _service.GetTaskByIdAsync(id);
            if (existing == null) return NotFound();

            await _service.UpdateTaskAsync(updatedTask);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existing = await _service.GetTaskByIdAsync(id);
            if (existing == null) return NotFound();

            await _service.DeleteTaskAsync(id);
            return NoContent();
        }
    }
}
