using FairDraw.App.Core.Entities;
using FairDraw.App.Infrastructure.TypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace FairDraw.App.Infrastructure;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<CompetitionEntity> Competitions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new CompetitionEntityTypeConfiguration().Configure(modelBuilder.Entity<CompetitionEntity>());
    }
}