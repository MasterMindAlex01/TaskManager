using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Tasks;

namespace TaskManager.Persistence.Configurations;

public class StatusHistoryConfig : IEntityTypeConfiguration<StatusHistory>
{
    public void Configure(EntityTypeBuilder<StatusHistory> builder)
    {
        builder.ToTable("StatusHistories");

        builder.HasKey(x => x.Id);

        builder.Property(e => e.PreviousStatus).HasMaxLength(100);

        builder.Property(e => e.NewStatus).HasMaxLength(100);

        builder.HasOne(d => d.User).WithMany(p => p.StatusHistories)
            .HasForeignKey(d => d.UserId);

        builder.HasOne(d => d.Task).WithMany(p => p.StatusHistories)
            .HasForeignKey(d => d.TaskId);
    }
}
