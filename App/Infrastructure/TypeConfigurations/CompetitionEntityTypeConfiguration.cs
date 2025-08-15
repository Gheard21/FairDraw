using FairDraw.App.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FairDraw.App.Infrastructure.TypeConfigurations
{
    internal class CompetitionEntityTypeConfiguration : IEntityTypeConfiguration<CompetitionEntity>
    {
        public void Configure(EntityTypeBuilder<CompetitionEntity> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(c => c.DateCreated)
                .IsRequired();
        }
    }
}
