using BacklogOrganizer.Modules.Backlogs.Core.Backlogs.Data;
using BacklogOrganizer.Modules.Backlogs.Core.Backlogs.Gaming;
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
    }
}
