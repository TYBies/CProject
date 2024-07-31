using FootballMatches.API.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class CompetitionConfiguration : IEntityTypeConfiguration<Competition>
{
    public void Configure(EntityTypeBuilder<Competition> builder)
    {
        builder.HasKey(c => c.CompetitionId);

        builder.Property(c => c.CompetitionName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.CompetitionType)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.StartDate).IsRequired();
        builder.Property(c => c.EndDate).IsRequired();

        builder.HasOne(c => c.Season)
               .WithOne(s => s.Competition)
               .HasForeignKey<Season>(s => s.CompetitionId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.Matches)
            .WithOne(m => m.Competition)
            .HasForeignKey(m => m.CompetitionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}