using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Identity;

namespace TaskManager.Persistence.Configurations;

public class UserRoleConfig : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("UserRoles");

        builder.HasKey(e => new { e.UserId, e.RoleId });

        builder.HasOne(d => d.User).WithMany(p => p.Roles)
            .HasForeignKey(d => d.UserId);
        
        builder.HasOne(d => d.Role).WithMany(p => p.Users)
            .HasForeignKey(d => d.RoleId);

    }
}
