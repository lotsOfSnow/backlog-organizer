using BacklogOrganizer.Modules.Backlogs.Core.Gaming;
using Microsoft.EntityFrameworkCore;

namespace BacklogOrganizer.Modules.Backlogs.Core.Data;

public interface IBacklogStorage
{
    DbSet<GamingBacklog> GamingBacklogs { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
