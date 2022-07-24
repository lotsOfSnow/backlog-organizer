using BacklogOrganizer.Modules.Backlogs.Core.Backlogs.Gaming;
using Microsoft.EntityFrameworkCore;

namespace BacklogOrganizer.Modules.Backlogs.Core.Backlogs.Data;

public interface IBacklogStorage
{
    DbSet<GamingBacklog> GamingBacklogs { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
