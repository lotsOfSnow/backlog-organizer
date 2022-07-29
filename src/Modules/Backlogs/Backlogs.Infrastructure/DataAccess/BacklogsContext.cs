using BacklogOrganizer.Modules.Backlogs.Core.Data;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming;
using BacklogOrganizer.Modules.Backlogs.Infrastructure.DataAccess.Configurations;
using Microsoft.EntityFrameworkCore;

namespace BacklogOrganizer.Modules.Backlogs.Infrastructure.DataAccess;

public class BacklogsContext : DbContext, IBacklogStorage
{
    public BacklogsContext(DbContextOptions<BacklogsContext> options) : base(options)
    {
    }

    public DbSet<GamingBacklog> GamingBacklogs { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GamingBacklog>().HasData(new GamingBacklog { Id = GamingBacklog.InstanceId });

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BacklogItemConfiguration).Assembly);
    }
}
