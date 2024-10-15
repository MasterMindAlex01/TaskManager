using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Identity;

namespace TaskManager.Persistence.Configurations;

public class RoleConfig : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");

        builder.HasKey(e => e.Id);

        builder.HasIndex(e => e.Name, "IX_role_name").IsUnique();

        builder.Property(e => e.Name).HasMaxLength(100);
        
        builder.Property(e => e.Description).HasMaxLength(500);
        
        builder.Property(e => e.IsDisabled);

    }
}
