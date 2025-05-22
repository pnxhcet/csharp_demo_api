using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using csharp_demo_api.Domain.Entities;

namespace csharp_demo_api.Infrastructure.Persistence.Configurations;

public class TaskEntityConfiguration : IEntityTypeConfiguration<TaskEntity>
{
    public void Configure(EntityTypeBuilder<TaskEntity> entity)
    {
        entity.ToTable("task", "demo");

        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id).HasColumnName("id");
        entity.Property(e => e.Title).HasMaxLength(255).IsRequired().HasColumnName("title");
        entity.Property(e => e.Description).IsRequired(false).HasColumnName("description");
        entity.Property(e => e.IsCompleted).IsRequired().HasColumnName("is_completed");
        entity.Property(e => e.DueDate).IsRequired(false).HasColumnName("due_date");
        entity.Property(e => e.CompletedAt).IsRequired(false).HasColumnName("completed_at");
        entity.Property(e => e.CreatedBy).HasMaxLength(255).IsRequired().HasColumnName("created_by");
        entity.Property(e => e.CreatedAt).IsRequired().HasColumnName("created_at");
    }
}
