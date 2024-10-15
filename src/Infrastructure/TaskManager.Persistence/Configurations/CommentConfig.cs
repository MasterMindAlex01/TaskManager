using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Tasks;

namespace TaskManager.Persistence.Configurations;

public class CommentConfig : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("Comments");

        builder.HasKey(x => x.Id);

        builder.HasOne(d => d.User).WithMany(p => p.Comments)
            .HasForeignKey(d => d.UserId);

        builder.HasOne(d => d.Task).WithMany(p => p.Comments)
            .HasForeignKey(d => d.TaskId);
    }
}
