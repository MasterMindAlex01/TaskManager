using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Identity;

namespace TaskManager.Persistence.Configurations;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        
        builder.HasKey(e => e.Id);

        builder.HasIndex(e => e.Username, "IX_username").IsUnique();
        
        builder.Property(e => e.Email)
            .HasMaxLength(100);
        
        builder.Property(e => e.Enabled);
        
        builder.Property(e => e.Firstname)
            .HasMaxLength(100);

        builder.Property(e => e.IsDeleted);
        
        builder.Property(e => e.Lastname)
            .HasMaxLength(100);
        
        builder.Property(e => e.Password)
            .HasMaxLength(255);
        
        builder.Property(e => e.PasswordSalt)
            .HasMaxLength(255);
        
        builder.Property(e => e.Username)
            .HasMaxLength(100);


    }
}
