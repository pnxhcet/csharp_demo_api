using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using csharp_demo_api.Domain.Entities;
using csharp_demo_api.Domain.Interfaces;
using csharp_demo_api.Infrastructure.Persistence;

namespace csharp_demo_api.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskEntity>> GetAllAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<TaskEntity?> GetByIdAsync(Guid id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task AddAsync(TaskEntity task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TaskEntity task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Tasks.FindAsync(id);
            if (entity != null)
            {
                _context.Tasks.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
