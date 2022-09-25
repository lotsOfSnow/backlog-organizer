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

    public DbSet<Backlog> Backlogs { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Backlog>().HasData(new Backlog { Id = Backlog.InstanceId });

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BacklogItemConfiguration).Assembly);
    }
}
