using FootballMatches.API.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class StadiumConfiguration : IEntityTypeConfiguration<Stadium>
{
    public void Configure(EntityTypeBuilder<Stadium> builder)
    {
        builder.HasKey(s => s.StadiumId);

        builder.Property(s => s.StadiumName)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasMany(s => s.Matches)
            .WithOne(m => m.Stadium)
            .HasForeignKey(m => m.StadiumId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}