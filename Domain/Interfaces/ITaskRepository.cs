using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using csharp_demo_api.Domain.Entities;

namespace csharp_demo_api.Domain.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskEntity>> GetAllAsync();
        Task<TaskEntity?> GetByIdAsync(Guid id);
        Task AddAsync(TaskEntity task);
        Task UpdateAsync(TaskEntity task);
        Task DeleteAsync(Guid id);
    }
}
