using FootballMatches.API.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FootballMatches.API.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            
            builder.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(50); 

            builder.Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(100); 

            builder.Property(u => u.Salt).HasMaxLength(100);
            builder.Property(u => u.CreatedAt).IsRequired();
            
            // Optional: add unique constraint on Username
            builder.HasIndex(u => u.Username)
                .IsUnique();
        }
    }
}
