using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using csharp_demo_api.Domain.Entities;
using csharp_demo_api.Domain.Interfaces;

namespace csharp_demo_api.Application.Services
{
    public class TaskService(ITaskRepository repository)
    {
        private readonly ITaskRepository _repository = repository;

        public Task<IEnumerable<TaskEntity>> GetAllTasksAsync()
        {
            return _repository.GetAllAsync();
        }

        public Task<TaskEntity?> GetTaskByIdAsync(Guid id)
        {
            return _repository.GetByIdAsync(id);
        }

        public Task AddTaskAsync(TaskEntity task)
        {
            task.Id = Guid.NewGuid();
            task.CreatedAt = DateTime.UtcNow;
            return _repository.AddAsync(task);
        }

        public Task UpdateTaskAsync(TaskEntity task)
        {
            return _repository.UpdateAsync(task);
        }

        public Task DeleteTaskAsync(Guid id)
        {
            return _repository.DeleteAsync(id);
        }
    }
}
