using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using csharp_demo_api.Domain.Entities;
using csharp_demo_api.Domain.Interfaces;
using csharp_demo_api.Infrastructure.Persistence;

namespace csharp_demo_api.Infrastructure.Repositories
{
    public class TaskRepositoryMongo(MongoDbContext context) : ITaskRepository
    {
        private readonly IMongoCollection<TaskEntity> _tasksCollection = context.Tasks;

        public async Task<IEnumerable<TaskEntity>> GetAllAsync()
        {
            return await _tasksCollection.Find(_ => true).ToListAsync();
        }

        public async Task<TaskEntity?> GetByIdAsync(Guid id)
        {
            return await _tasksCollection.Find(task => task.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddAsync(TaskEntity task)
        {
            await _tasksCollection.InsertOneAsync(task);
        }

        public async Task UpdateAsync(TaskEntity task)
        {
            await _tasksCollection.ReplaceOneAsync(t => t.Id == task.Id, task);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _tasksCollection.DeleteOneAsync(task => task.Id == id);
        }
    }
}
