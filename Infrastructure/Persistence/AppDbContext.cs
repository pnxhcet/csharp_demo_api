using csharp_demo_api.Domain.Entities;
using csharp_demo_api.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace csharp_demo_api.Infrastructure.Persistence
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<TaskEntity> Tasks { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TaskEntityConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
