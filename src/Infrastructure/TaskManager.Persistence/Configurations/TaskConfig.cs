using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskManager.Persistence.Configurations;

public class TaskConfig : IEntityTypeConfiguration<Domain.Tasks.Task>
{
    public void Configure(EntityTypeBuilder<Domain.Tasks.Task> builder)
    {
        builder.ToTable("Tasks");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Title).IsRequired().HasMaxLength(500);

        builder.Property(e => e.Description).HasMaxLength(500);

        builder.Property(e => e.Priority).HasMaxLength(100);

        builder.Property(e => e.Priority).HasMaxLength(250);
    }
}
