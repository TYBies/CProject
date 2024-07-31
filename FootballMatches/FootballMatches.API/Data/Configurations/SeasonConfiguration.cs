using FootballMatches.API.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class SeasonConfiguration : IEntityTypeConfiguration<Season>
{
    public void Configure(EntityTypeBuilder<Season> builder)
    {
        builder.HasKey(s => s.SeasonId);

        builder.Property(s => s.SeasonName)
            .IsRequired()
            .HasMaxLength(50);
       
        builder.HasOne(s => s.Competition)
               .WithOne(c => c.Season)
               .HasForeignKey<Season>(s => s.CompetitionId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}