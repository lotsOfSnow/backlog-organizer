using BacklogOrganizer.Modules.Backlogs.Core.Data;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Groups;
using BacklogOrganizer.Modules.Backlogs.Infrastructure.DataAccess.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BacklogOrganizer.Modules.Backlogs.Infrastructure.DataAccess;

public class BacklogsContext : DbContext, IBacklogStorage
{
    public BacklogsContext(DbContextOptions<BacklogsContext> options) : base(options)
    {
    }

    public DbSet<Backlog> Backlogs { get; private set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        DisableAutoGeneratingIds(modelBuilder);

        modelBuilder.Entity<Backlog>().HasData(new Backlog(Backlog.InstanceId));

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BacklogItemConfiguration).Assembly);
    }

    private static void DisableAutoGeneratingIds(ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entity.GetProperties().Where(p => p.IsPrimaryKey()))
            {
                property.ValueGenerated = ValueGenerated.Never;
            }
        }
    }

    private DbSet<BacklogGroup> Groups { get; } = null!;
}
